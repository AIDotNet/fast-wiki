using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using FastWiki.Service.Contracts.Feishu.Dto;
using FastWiki.Service.Contracts.Model.Dto;
using FastWiki.Service.Contracts.OpenAI;
using FastWiki.Service.Domain.Function.Repositories;
using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FastWiki.Service.Service;

public class FeishuService
{
    private static readonly HttpClient httpClient = new();

    public static JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private static readonly ConcurrentDictionary<string, DateTime> MemoryCache = new();

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
                    en_US = "You have open Encrypt Key Feature, please close it."
                }
            });
            return;
        }


        // 处理飞书开放平台的服务端校验
        if (input.type == "url_verification")
        {
            await context.Response.WriteAsJsonAsync(new
            {
                input.challenge
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
                    code = 0
                });

                return;
            }

            memoryCache.Set(eventId, true, TimeSpan.FromHours(1));

            var eventBus = context.RequestServices.GetRequiredService<IEventBus>();
            var wikiMemoryService = context.RequestServices.GetRequiredService<WikiMemoryService>();
            var chatShareInfoQuery = new ChatShareInfoQuery(id);

            await eventBus.PublishAsync(chatShareInfoQuery);

            var getApiKeyChatShareQuery = new GetAPIKeyChatShareQuery(string.Empty)
            {
                // 如果chatShareId不存在则返回让下面扣款
                Result = chatShareInfoQuery.Result
            };

            var chatApplicationQuery = new ChatApplicationInfoQuery(chatShareInfoQuery.Result.ChatApplicationId);

            await eventBus.PublishAsync(chatApplicationQuery);

            var chatApplication = chatApplicationQuery?.Result;

            if (chatApplication == null)
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    code = 2
                });
                return;
            }

            var fastWikiFunctionCallRepository =
                context.RequestServices.GetRequiredService<IFastWikiFunctionCallRepository>();
            var fileStorageRepository = context.RequestServices.GetRequiredService<IFileStorageRepository>();

            // 私聊直接回复
            if (input._event.message.chat_type == "p2p")
            {
                // 不是文本消息，不处理
                if (input._event.message.message_type != "text")
                {
                    await SendMessages(chatApplication, messageId, "暂不支持其他类型的提问");
                    await context.Response.WriteAsJsonAsync(new
                    {
                        code = 0
                    });
                    return;
                }

                // 是文本消息，直接回复
                var userInput = JsonSerializer.Deserialize<FeishuChatUserInput>(input._event.message.content);

                await ChatMessage(context, userInput.text, sessionId, chatApplication, chatShareInfoQuery,
                    eventBus, wikiMemoryService, fileStorageRepository, fastWikiFunctionCallRepository);

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
                        code = 0
                    });
                    return;
                }


                // 没有 mention 机器人，则退出。
                if (input._event.message.mentions[0].name != chatApplication.GetBotName())
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        code = 0
                    });
                    return;
                }

                // 是文本消息，直接回复
                var userInput = JsonSerializer.Deserialize<FeishuChatUserInput>(input._event.message.content);

                var history = new ChatHistory();
                history.AddUserMessage(userInput.text);

                await ChatMessage(context, userInput.text, sessionId, chatApplication, chatShareInfoQuery,
                    eventBus, wikiMemoryService, fileStorageRepository, fastWikiFunctionCallRepository);

                return;
            }
        }

        await context.Response.WriteAsJsonAsync(new
        {
            code = 2
        });
    }

    public static async Task ChatMessage(HttpContext context, string content, string sessionId,
        ChatApplicationDto chatApplication, ChatShareInfoQuery chatShareInfoQuery, IEventBus eventBus,
        WikiMemoryService wikiMemoryService,
        IFileStorageRepository fileStorageRepository,
        IFastWikiFunctionCallRepository fastWikiFunctionCallRepository)
    {
        var requestToken = 0;

        var module = new ChatCompletionDto<ChatCompletionRequestMessage>
        {
            messages =
            [
                new ChatCompletionRequestMessage
                {
                    content = content,
                    role = "user"
                }
            ]
        };

        var chatHistory = new ChatHistory();

        // 如果设置了Prompt，则添加
        if (!chatApplication.Prompt.IsNullOrEmpty()) chatHistory.AddSystemMessage(chatApplication.Prompt);

        // 保存对话提问
        var createChatRecordCommand =
            new CreateChatRecordCommand(chatApplication.Id, content);

        await eventBus.PublishAsync(createChatRecordCommand);

        var sourceFile = new List<FileStorage>();
        var memoryServerless = wikiMemoryService.CreateMemoryServerless(chatApplication.ChatModel);

        // 如果为空则不使用知识库
        if (chatApplication.WikiIds.Count != 0)
        {
            var success = await OpenAIService.WikiPrompt(chatApplication, memoryServerless,
                content,
                eventBus, fileStorageRepository,
                sourceFile, module);

            if (!success) return;
        }

        var output = new StringBuilder();

        // 添加用户输入，并且计算请求token数量
        module.messages.ForEach(x =>
        {
            if (x.content.IsNullOrEmpty()) return;
            requestToken += TokenHelper.ComputeToken(x.content);

            chatHistory.Add(new ChatMessageContent(new AuthorRole(x.role), x.content));
        });


        if (chatShareInfoQuery.Result != null)
        {
            // 如果token不足则返回，使用token和当前request总和大于可用token，则返回
            if (chatShareInfoQuery.Result.AvailableToken != -1 &&
                chatShareInfoQuery.Result.UsedToken + requestToken >=
                chatShareInfoQuery.Result.AvailableToken)
            {
                output.Append("Token不足");
                return;
            }

            // 如果没有过期则继续
            if (chatShareInfoQuery.Result.Expires != null &&
                chatShareInfoQuery.Result.Expires < DateTimeOffset.Now)
            {
                output.Append("Token已过期");
                return;
            }
        }

        // 是文本消息，直接回复
        var userInput = JsonSerializer.Deserialize<FeishuChatUserInput>(content);

        var history = new ChatHistory();
        history.AddUserMessage(userInput.text);

        var text = new StringBuilder();

        try
        {
            await foreach (var item in OpenAIService.SendChatMessageAsync(chatApplication,
                               wikiMemoryService,
                               chatHistory, fastWikiFunctionCallRepository))
            {
                if (string.IsNullOrEmpty(item)) continue;

                output.Append(item);
            }

            //对于对话扣款
            if (chatShareInfoQuery.Result != null)
            {
                var updateChatShareCommand = new DeductTokenCommand(chatShareInfoQuery.Result.Id,
                    requestToken);

                await eventBus.PublishAsync(updateChatShareCommand);
            }
        }
        catch (InvalidOperationException invalidOperationException)
        {
            output.Clear();
            output.Append("对话异常:" + invalidOperationException.Message);
        }
        catch (ArgumentException argumentException)
        {
            output.Clear();
            output.Append("对话异常:" + argumentException.Message);
        }
        catch (Exception e)
        {
            output.Clear();
            output.Append("对话异常,请联系管理员");
        }

        await SendMessages(chatApplication, sessionId, text.ToString());

        await context.Response.WriteAsJsonAsync(new
        {
            code = 0
        });
    }

    public static async ValueTask SendMessages(ChatApplicationDto chatApplication, string sessionId, string message,
        string receive_id_type = "open_id")
    {
        await SendMessages(chatApplication, new FeishuChatSendMessageInput(JsonSerializer.Serialize(new
        {
            text = message
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

        if (result?.code != 0) throw new UserFriendlyException(result?.msg);
    }

    private static async ValueTask RefreshTokenAsync(HttpRequestMessage requestMessage,
        ChatApplicationDto chatApplication)
    {
        if (requestMessage.Headers.Contains("Authorization"))
        {
            if (!MemoryCache.TryGetValue(chatApplication.Id, out var _lastTime))
                MemoryCache.TryUpdate(chatApplication.Id, DateTime.Now, _lastTime);
            else if (_lastTime != null && DateTime.Now - _lastTime < TimeSpan.FromHours(1.5))
                // 如果LastTime大于1.5小时，刷新token
                return;

            requestMessage.Headers.Remove("Authorization");
        }


        var request = new HttpRequestMessage(HttpMethod.Post,
            "https://open.feishu.cn/open-apis/auth/v3/app_access_token/internal");

        request.Content = new StringContent(JsonSerializer.Serialize(new
        {
            app_id = chatApplication.GetFeishuAppId(),
            app_secret = chatApplication.GetFeishuAppSecret()
        }), Encoding.UTF8, "application/json");
        var response = await httpClient.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<FeiShuChatToken>(result);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.TenantAccessToken);

        MemoryCache.Remove(chatApplication.Id, out _);
        MemoryCache.TryAdd(chatApplication.Id, DateTime.Now);
    }
}