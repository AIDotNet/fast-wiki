using FastWiki.Service.Application.Storage.Queries;
using FastWiki.Service.Contracts.Feishu.Dto;
using FastWiki.Service.Contracts.Model.Dto;
using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Infrastructure;
using FastWiki.Service.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using TokenApi.Service.Exceptions;

namespace FastWiki.Service.Service;

public class FeishuService
{
    private static HttpClient httpClient = new();

    public static JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    public static async Task Completions(string id, HttpContext context, [FromBody] FeishuChatInput input)
    {
        var memoryCache = context.RequestServices.GetRequiredService<IMemoryCache>();

        if (!input.encrypt.IsNullOrWhiteSpace())
        {
            await context.Response.WriteAsJsonAsync(new
            {
                code = 1,
                message = new
                {
                    zh_CN = "你配置了 Encrypt Key，请关闭该功能。",
                    en_US = "You have open Encrypt Key Feature, please close it.",
                }
            });
            return;
        }


        // 处理飞书开放平台的服务端校验
        if (input.type == "url_verification")
        {
            await context.Response.WriteAsJsonAsync(new
            {
                input.challenge,
            });
            return;
        }

        // 处理飞书开放平台的事件回调
        if (input.header.event_type == "im.message.receive_v1")
        {
            var eventId = input.header.event_id; // 事件id
            var messageId = input._event.message.message_id; // 消息id
            var chatId = input._event.message.chat_id; // 群聊id
            var senderId = input._event.sender.sender_id.user_id; // 发送人id
            var sessionId = input._event.sender.sender_id.open_id; // 发送人openid

            Console.WriteLine(eventId);
            if (memoryCache.TryGetValue(eventId, out var eventValue))
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    code = 0,
                });

                return;
            }

            memoryCache.Set(eventId, true, TimeSpan.FromHours(1));

            var eventBus = context.RequestServices.GetRequiredService<IEventBus>();
            var chatShareInfoQuery = new ChatShareInfoQuery(id);

            await eventBus.PublishAsync(chatShareInfoQuery);

            var getApiKeyChatShareQuery = new GetAPIKeyChatShareQuery(string.Empty);
            // 如果chatShareId不存在则返回让下面扣款
            getApiKeyChatShareQuery.Result = chatShareInfoQuery.Result;

            var chatApplicationQuery = new ChatApplicationInfoQuery(chatShareInfoQuery.Result.ChatApplicationId);

            await eventBus.PublishAsync(chatApplicationQuery);

            var chatApplication = chatApplicationQuery?.Result;

            if (chatApplication == null)
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    code = 2,
                });
                return;
            }

            // 私聊直接回复
            if (input._event.message.chat_type == "p2p")
            {
                // 不是文本消息，不处理
                if (input._event.message.message_type != "text")
                {
                    await SendMessages(chatApplication, messageId, "暂不支持其他类型的提问");
                    await context.Response.WriteAsJsonAsync(new
                    {
                        code = 0,
                    });
                    return;
                }


                // 是文本消息，直接回复
                var userInput = JsonSerializer.Deserialize<FeishuChatUserInput>(input._event.message.content);

                var history = new ChatHistory();
                history.AddUserMessage(userInput.text);

                var text = new StringBuilder();

                await ChatMessageAsync(getApiKeyChatShareQuery, chatApplication, context, history, id,
                    async x => { text.Append(x); });

                await SendMessages(chatApplication, sessionId, text.ToString());

                await context.Response.WriteAsJsonAsync(new
                {
                    code = 0,
                });
                return;
            }


            // 群聊，需要 @ 机器人
            if (input._event.message.chat_type == "group")
            {
                // 这是日常群沟通，不用管
                if (input._event.message.mentions.Length == 0)
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        code = 0,
                    });
                    return;
                }


                // 没有 mention 机器人，则退出。
                if (input._event.message.mentions[0].name != chatApplication.GetBotName())
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        code = 0,
                    });
                    return;
                }


                // 是文本消息，直接回复
                var userInput = JsonSerializer.Deserialize<FeishuChatUserInput>(input._event.message.content);

                var history = new ChatHistory();
                history.AddUserMessage(userInput.text);

                var text = new StringBuilder();

                await ChatMessageAsync(getApiKeyChatShareQuery, chatApplication, context, history, id,
                    async x => { text.Append(x); });

                await SendMessages(chatApplication, chatId, text.ToString(), "chat_id");

                await context.Response.WriteAsJsonAsync(new
                {
                    code = 0,
                });
                return;
            }
        }

        await context.Response.WriteAsJsonAsync(new
        {
            code = 2,
        });
    }


    public static async Task ChatMessageAsync(GetAPIKeyChatShareQuery getAPIKeyChatShareQuery,
        ChatApplicationDto chatApplication, HttpContext context,
        ChatHistory history, string chatShareId,
        Action<string> chatResponse)
    {
        var eventBus = context.RequestServices.GetRequiredService<IEventBus>();

        // 如果设置了Prompt，则添加
        if (!chatApplication.Prompt.IsNullOrEmpty())
        {
            history.AddSystemMessage(chatApplication.Prompt);
        }


        var content = history.Last();
        var question = content.Content;

        var prompt = string.Empty;

        var sourceFile = new List<FileStorage>();
        // 如果为空则不使用知识库
        if (chatApplication.WikiIds.Count != 0)
        {
            var memoryServerless =
                context.RequestServices.GetRequiredService<WikiMemoryService>().CreateMemoryServerless();

            var filters = chatApplication.WikiIds
                .Select(chatApplication => new MemoryFilter().ByTag("wikiId", chatApplication.ToString())).ToList();

            var result = await memoryServerless.SearchAsync(content.Content, "wiki", filters: filters, limit: 3,
                minRelevance: chatApplication.Relevancy);

            var fileIds = new List<long>();

            result.Results.ForEach(x =>
            {
                // 获取fileId
                var fileId = x.Partitions.Select(x => x.Tags.FirstOrDefault(x => x.Key == "fileId"))
                    .FirstOrDefault(x => !x.Value.IsNullOrEmpty())
                    .Value.FirstOrDefault();

                if (!fileId.IsNullOrWhiteSpace() && long.TryParse(fileId, out var id))
                {
                    fileIds.Add(id);
                }

                prompt += string.Join(Environment.NewLine, x.Partitions.Select(x => x.Text));
            });

            if (result.Results.Count == 0 &&
                !string.IsNullOrWhiteSpace(chatApplication.NoReplyFoundTemplate))
            {
                await context.WriteEndAsync(chatApplication.NoReplyFoundTemplate);
                return;
            }

            var tokens = TokenHelper.GetGptEncoding().Encode(prompt);

            // 这里可以有效的防止token数量超出限制，但是也会降低回复的质量
            prompt = TokenHelper.GetGptEncoding()
                .Decode(tokens.Take(chatApplication.MaxResponseToken).ToList());

            // 如果prompt不为空，则需要进行模板替换
            if (!prompt.IsNullOrEmpty())
            {
                prompt = chatApplication.Template.Replace("{{quote}}", prompt)
                    .Replace("{{question}}", content.Content);
            }

            // 在这里需要获取源文件
            if (fileIds.Count > 0 && chatApplication.ShowSourceFile)
            {
                var fileQuery = new StorageInfosQuery(fileIds);

                await eventBus.PublishAsync(fileQuery);

                sourceFile.AddRange(fileQuery.Result);
            }

            if (!prompt.IsNullOrEmpty())
            {
                // 删除最后一个消息
                history.RemoveAt(history.Count - 1);
                history.AddUserMessage(prompt);
            }
        }

        // 添加用户输入，并且计算请求token数量
        int requestToken = history.Where(x => !x.Content.IsNullOrEmpty()).Sum(x => TokenHelper.ComputeToken(x.Content));


        if (getAPIKeyChatShareQuery?.Result != null)
        {
            // 如果token不足则返回，使用token和当前request总和大于可用token，则返回
            if (getAPIKeyChatShareQuery.Result.AvailableToken != -1 &&
                (getAPIKeyChatShareQuery.Result.UsedToken + requestToken) >=
                getAPIKeyChatShareQuery.Result.AvailableToken)
            {
                await context.WriteEndAsync("Token不足");
                return;
            }

            // 如果没有过期则继续
            if (getAPIKeyChatShareQuery.Result.Expires != null &&
                getAPIKeyChatShareQuery.Result.Expires < DateTimeOffset.Now)
            {
                await context.WriteEndAsync("Token已过期");
                return;
            }
        }

        var responseId = Guid.NewGuid().ToString("N");
        var requestId = Guid.NewGuid().ToString("N");
        var output = new StringBuilder();
        try
        {
            var kernel = context.RequestServices.GetRequiredService<Kernel>();

            var chat = kernel.GetRequiredService<IChatCompletionService>();

            await foreach (var item in chat.GetStreamingChatMessageContentsAsync(history))
            {
                var message = item.Content;
                if (string.IsNullOrEmpty(message))
                {
                    continue;
                }

                output.Append(message);

                chatResponse.Invoke(message);
            }
        }
        catch (NotModelException notModelException)
        {
            chatResponse.Invoke("未找到模型兼容：" + notModelException.Message);
            return;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            chatResponse.Invoke("对话异常：" + invalidOperationException.Message);
            return;
        }
        catch (ArgumentException argumentException)
        {
            chatResponse.Invoke(argumentException.Message);
            return;
        }
        catch (Exception e)
        {
            chatResponse.Invoke("对话异常：" + e.Message);
            return;
        }

        #region 记录对话内容

        var createChatDialogHistoryCommand = new CreateChatDialogHistoryCommand(new CreateChatDialogHistoryInput()
        {
            ChatDialogId = string.Empty,
            Id = requestId,
            Content = question,
            ExpendToken = requestToken,
            Type = ChatDialogHistoryType.Text,
            Current = true
        });

        await eventBus.PublishAsync(createChatDialogHistoryCommand);

        var outputContent = output.ToString();
        var completeToken = TokenHelper.ComputeToken(outputContent);

        var chatDialogHistory = new CreateChatDialogHistoryCommand(new CreateChatDialogHistoryInput()
        {
            ChatDialogId = string.Empty,
            Content = outputContent,
            Id = responseId,
            ExpendToken = completeToken,
            Type = ChatDialogHistoryType.Text,
            Current = false,
            ReferenceFile = sourceFile.Select(x => new SourceFileDto()
            {
                Name = x.Name,
                FileId = x.Id.ToString(),
                FilePath = x.Path
            }).ToList()
        });

        await eventBus.PublishAsync(chatDialogHistory);

        #endregion

        //对于对话扣款
        if (getAPIKeyChatShareQuery?.Result != null)
        {
            var updateChatShareCommand = new DeductTokenCommand(getAPIKeyChatShareQuery.Result.Id,
                requestToken);

            await eventBus.PublishAsync(updateChatShareCommand);
        }
    }

    private static readonly ConcurrentDictionary<string, DateTime> MemoryCache = new();

    public static async ValueTask SendMessages(ChatApplicationDto chatApplication, string sessionId, string message,
        string receive_id_type = "open_id")
    {
        await SendMessages(chatApplication, new FeishuChatSendMessageInput(JsonSerializer.Serialize(new
        {
            text = message,
        }, JsonSerializerOptions), "text", sessionId), receive_id_type);
    }

    private static async ValueTask SendMessages(ChatApplicationDto chatApplication, FeishuChatSendMessageInput input,
        string receive_id_type = "open_id")
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post,
            "https://open.feishu.cn/open-apis/im/v1/messages?receive_id_type=" + receive_id_type);

        requestMessage.Content = new StringContent(JsonSerializer.Serialize(input, JsonSerializerOptions),
            Encoding.UTF8, "application/json");

        await RefreshTokenAsync(requestMessage, chatApplication);

        var response = await httpClient.SendAsync(requestMessage);

        var result = await response.Content.ReadFromJsonAsync<FeiShuChatResult>();

        if (result?.code != 0)
        {
            throw new UserFriendlyException(result?.msg);
        }
    }

    private static async ValueTask RefreshTokenAsync(HttpRequestMessage requestMessage,
        ChatApplicationDto chatApplication)
    {
        if (requestMessage.Headers.Contains("Authorization"))
        {
            if (!MemoryCache.TryGetValue(chatApplication.Id, out var _lastTime))
            {
                MemoryCache.TryUpdate(chatApplication.Id, DateTime.Now, _lastTime);
            }
            else if (_lastTime != null && DateTime.Now - _lastTime < TimeSpan.FromHours(1.5))
            {
                // 如果LastTime大于1.5小时，刷新token
                return;
            }

            requestMessage.Headers.Remove("Authorization");
        }


        var request = new HttpRequestMessage(HttpMethod.Post,
            "https://open.feishu.cn/open-apis/auth/v3/app_access_token/internal");

        request.Content = new StringContent(JsonSerializer.Serialize(new
        {
            app_id = chatApplication.GetFeishuAppId(),
            app_secret = chatApplication.GetFeishuAppSecret(),
        }), Encoding.UTF8, "application/json");
        var response = await httpClient.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<FeiShuChatToken>(result);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.TenantAccessToken);

        MemoryCache.Remove(chatApplication.Id, out _);
        MemoryCache.TryAdd(chatApplication.Id, DateTime.Now);
    }
}