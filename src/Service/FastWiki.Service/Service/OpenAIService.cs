using System.Text;
using System.Text.Json;
using Azure.AI.OpenAI;
using FastWiki.Service.Application.Function.Queries;
using FastWiki.Service.Application.Storage.Queries;
using FastWiki.Service.Contracts.OpenAI;
using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Infrastructure;
using FastWiki.Service.Infrastructure.Helper;
using Microsoft.KernelMemory.DataFormats.Text;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using TokenApi.Service.Exceptions;

namespace FastWiki.Service.Service;

public class OpenAIService
{
    /// <summary>
    /// ChatCompletion 
    /// </summary>
    /// <param name="context"></param>
    public static async Task Completions(HttpContext context)
    {
        using var stream = new StreamReader(context.Request.Body);

        var module =
            JsonSerializer.Deserialize<ChatCompletionDto<ChatCompletionRequestMessage>>(await stream.ReadToEndAsync(),
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });

        context.Response.ContentType = "text/event-stream";

        if (module == null)
        {
            await context.WriteEndAsync("Invalid request");

            return;
        }

        var logger = context.RequestServices.GetRequiredService<ILogger<OpenAIService>>();

        var chatId = context.Request.Query["ChatId"].ToString();
        var token = context.Request.Headers.Authorization;

        if (chatId.IsNullOrEmpty())
        {
            chatId = module.ApplicationId;
        }
        
        if(chatId.IsNullOrEmpty())
        {
            chatId = context.Request.Headers["applicationId"];
        }

        // 获取分享Id
        var chatShareId = context.Request.Query["ChatShareId"].ToString();
        if (chatShareId.IsNullOrEmpty())
        {
            chatShareId = module.SharedId;
        }
        
        if(chatShareId.IsNullOrEmpty())
        {
            chatShareId = context.Request.Headers["sharedId"];
        }

        var eventBus = context.RequestServices.GetRequiredService<IEventBus>();

        var getAPIKeyChatShareQuery = new GetAPIKeyChatShareQuery(token);

        ChatApplicationDto chatApplication = null;

        if (token.ToString().Replace("Bearer ", "").Trim().StartsWith("sk-"))
        {
            getAPIKeyChatShareQuery = new GetAPIKeyChatShareQuery(token.ToString().Replace("Bearer ", "").Trim());
            await eventBus.PublishAsync(getAPIKeyChatShareQuery);

            if (getAPIKeyChatShareQuery.Result == null)
            {
                context.Response.StatusCode = 401;
                await context.WriteEndAsync("Token无效");
                return;
            }

            var chatApplicationQuery = new ChatApplicationInfoQuery(getAPIKeyChatShareQuery.Result.ChatApplicationId);

            await eventBus.PublishAsync(chatApplicationQuery);

            chatApplication = chatApplicationQuery?.Result;
        }
        else
        {
            // 如果不是sk则校验用户 并且不是分享链接
            if (chatShareId.IsNullOrEmpty())
            {
                // 判断当前用户是否登录
                if (context.User.Identity?.IsAuthenticated == false)
                {
                    context.Response.StatusCode = 401;
                    await context.WriteEndAsync("Token不能为空");
                    return;
                }
            }

            // 如果是分享链接则获取分享信息
            if (!chatShareId.IsNullOrEmpty())
            {
                var chatShareInfoQuery = new ChatShareInfoQuery(chatShareId);

                await eventBus.PublishAsync(chatShareInfoQuery);

                // 如果chatShareId不存在则返回让下面扣款
                getAPIKeyChatShareQuery.Result = chatShareInfoQuery.Result;

                var chatApplicationQuery = new ChatApplicationInfoQuery(chatShareInfoQuery.Result.ChatApplicationId);

                await eventBus.PublishAsync(chatApplicationQuery);

                chatApplication = chatApplicationQuery?.Result;
            }
            // 如果是应用Id则获取应用信息
            else if (!chatId.IsNullOrEmpty())
            {
                var chatApplicationQuery = new ChatApplicationInfoQuery(chatId);
                await eventBus.PublishAsync(chatApplicationQuery);
                chatApplication = chatApplicationQuery?.Result;
            }

            if (chatApplication == null)
            {
                await context.WriteEndAsync("应用Id不存在");
                return;
            }
        }

        int requestToken = 0;

        var chatHistory = new ChatHistory();

        // 如果设置了Prompt，则添加
        if (!chatApplication.Prompt.IsNullOrEmpty())
        {
            chatHistory.AddSystemMessage(chatApplication.Prompt);
        }


        var content = module.messages.Last();
        var question = content.content;

        // 保存对话提问
        var createChatRecordCommand = new CreateChatRecordCommand(chatApplication.Id, question);

        await eventBus.PublishAsync(createChatRecordCommand);

        var sourceFile = new List<FileStorage>();
        var wikiMemoryService = context.RequestServices.GetRequiredService<WikiMemoryService>();
        var memoryServerless = wikiMemoryService.CreateMemoryServerless(chatApplication.ChatModel);

        // 如果为空则不使用知识库
        if (chatApplication.WikiIds.Count != 0)
        {
            var success = await WikiPrompt(chatApplication, memoryServerless, content.content, eventBus,
                sourceFile, module, async x => { await context.WriteEndAsync(x); });

            if (!success)
            {
                return;
            }
        }

        // 添加用户输入，并且计算请求token数量
        module.messages.ForEach(x =>
        {
            if (x.content.IsNullOrEmpty()) return;
            requestToken += TokenHelper.ComputeToken(x.content);

            chatHistory.Add(new ChatMessageContent(new AuthorRole(x.role), x.content));
        });


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
            await foreach (var item in SendChatMessageAsync(chatApplication, eventBus, wikiMemoryService, chatHistory))
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }

                output.Append(item);
                await context.WriteOpenAiResultAsync(item, module.model, requestId,
                    responseId);
            }
        }
        catch (NotModelException notModelException)
        {
            await context.WriteEndAsync("未找到模型兼容：" + notModelException.Message);
            logger.LogError(notModelException, "未找到模型兼容");
            return;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            await context.WriteEndAsync("对话异常：" + invalidOperationException.Message);
            logger.LogError(invalidOperationException, "对话异常");
            return;
        }
        catch (ArgumentException argumentException)
        {
            await context.WriteEndAsync(argumentException.Message);
            logger.LogError(argumentException, "对话异常");
            return;
        }
        catch (Exception e)
        {
            logger.LogError(e, "对话异常");
            await context.WriteEndAsync("对话异常：" + e.Message);
            return;
        }

        await context.WriteEndAsync();

        //对于对话扣款
        if (getAPIKeyChatShareQuery?.Result != null)
        {
            var updateChatShareCommand = new DeductTokenCommand(getAPIKeyChatShareQuery.Result.Id,
                requestToken);

            await eventBus.PublishAsync(updateChatShareCommand);
        }
    }

    /// <summary>
    /// 提问AI
    /// </summary>
    /// <param name="chatApplication"></param>
    /// <param name="eventBus"></param>
    /// <param name="wikiMemoryService"></param>
    /// <param name="chatHistory"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<string> SendChatMessageAsync(ChatApplicationDto chatApplication,
        IEventBus eventBus, WikiMemoryService wikiMemoryService, ChatHistory chatHistory)
    {
        var functionCall = new ChatApplicationFunctionCallQuery(chatApplication.FunctionIds.ToArray());

        if (chatApplication.FunctionIds.Any())
        {
            await eventBus.PublishAsync(functionCall);
        }

        var kernel =
            wikiMemoryService.CreateFunctionKernel(functionCall?.Result?.ToList(), chatApplication.ChatModel);

        // 如果有函数调用
        if (chatApplication.FunctionIds.Any() && functionCall.Result.Any())
        {
            OpenAIPromptExecutionSettings settings = new()
            {
                ToolCallBehavior = ToolCallBehavior.EnableKernelFunctions
            };

            var chat = kernel.GetRequiredService<IChatCompletionService>();

            var result =
                (OpenAIChatMessageContent)await chat.GetChatMessageContentAsync(chatHistory, settings,
                    kernel);

            List<ChatCompletionsFunctionToolCall> toolCalls =
                result.ToolCalls.OfType<ChatCompletionsFunctionToolCall>().ToList();

            if (toolCalls.Count == 0)
            {
                yield return "未找到函数";
                yield break;
            }

            foreach (var toolCall in toolCalls)
            {
                kernel.Plugins.TryGetFunctionAndArguments(toolCall, out var function,
                    out KernelArguments? arguments);

                if (function == null)
                {
                    continue;
                }

                Exception? exception = null;

                try
                {
                    var functionResult = await function.InvokeAsync(kernel, new KernelArguments()
                    {
                        {
                            "value", arguments?.Select(x => x.Value).ToArray()
                        }
                    });
                    // 判断ValueType是否为值类型
                    if (functionResult.ValueType?.IsValueType == true || functionResult.ValueType == typeof(string))
                    {
                        chatHistory.AddAssistantMessage(functionResult.GetValue<object>().ToString());
                    }
                    else
                    {
                        // 记录函数调用
                        chatHistory.AddAssistantMessage(
                            JsonSerializer.Serialize(functionResult.GetValue<object>()));
                    }
                }
                catch (Exception e)
                {
                    exception = e;
                }

                if (exception != null)
                {
                    yield return "函数调用异常：" + exception?.Message;
                    yield break;
                }
            }

            await foreach (var message in SendChatMessageAsync(chat, chatHistory))
            {
                if (string.IsNullOrEmpty(message))
                {
                    continue;
                }

                yield return message;
            }
        }
        else
        {
            var chat = kernel.GetRequiredService<IChatCompletionService>();

            await foreach (var item in chat.GetStreamingChatMessageContentsAsync(chatHistory))
            {
                var message = item.Content;
                if (string.IsNullOrEmpty(message))
                {
                    continue;
                }

                yield return message;
            }
        }
    }

    public static async IAsyncEnumerable<string> SendChatMessageAsync(IChatCompletionService chat,
        ChatHistory chatHistory)
    {
        await foreach (var item in chat.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            yield return item.Content;
        }
    }

    /// <summary>
    /// 知识库Prompt组合
    /// 在向量中搜索响应的知识库内容，然后将其添加到对话中
    /// </summary>
    /// <param name="context"></param>
    /// <param name="chatApplication"></param>
    /// <param name="memoryServerless"></param>
    /// <param name="content"></param>
    /// <param name="eventBus"></param>
    /// <param name="sourceFile"></param>
    /// <param name="module"></param>
    /// <param name="notFoundAction"></param>
    /// <returns></returns>
    public static async ValueTask<bool> WikiPrompt(ChatApplicationDto chatApplication,
        MemoryServerless memoryServerless, string content, IEventBus eventBus,
        List<FileStorage> sourceFile, ChatCompletionDto<ChatCompletionRequestMessage> module,
        Func<string, ValueTask> notFoundAction = null)
    {
        var prompt = string.Empty;
        var filters = chatApplication.WikiIds
            .Select(chatApplication => new MemoryFilter().ByTag("wikiId", chatApplication.ToString())).ToList();

        var result = await memoryServerless.SearchAsync(content, "wiki", filters: filters, limit: 3,
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
            await notFoundAction!.Invoke(chatApplication.NoReplyFoundTemplate);
            return false;
        }

        var tokens = TokenHelper.GetGptEncoding().Encode(prompt);

        // 这里可以有效的防止token数量超出限制，但是也会降低回复的质量
        prompt = TokenHelper.GetGptEncoding()
            .Decode(tokens.Take(chatApplication.MaxResponseToken).ToList());

        // 如果prompt不为空，则需要进行模板替换
        if (!prompt.IsNullOrEmpty())
        {
            prompt = chatApplication.Template.Replace("{{quote}}", prompt)
                .Replace("{{question}}", content);
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
            module.messages.RemoveAt(module.messages.Count - 1);
            module.messages.Add(new ChatCompletionRequestMessage()
            {
                content = prompt,
                role = "user"
            });
        }

        return true;
    }

    /// <summary>
    /// QA问答解析大文本拆分多个段落
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="value"></param>
    /// <param name="model"></param>
    /// <param name="apiKey"></param>
    /// <param name="url"></param>
    /// <param name="memoryService"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<string> QaAsync(string prompt, string value, string model, string apiKey,
        string url,
        WikiMemoryService memoryService)
    {
        var kernel = memoryService.CreateFunctionKernel(apiKey, model, url);

        var qaFunction = kernel.CreateFunctionFromPrompt(prompt, functionName: "QA", description: "QA问答");


        #pragma warning disable KMEXP00 // by design
        var lines = TextChunker.SplitPlainTextLines(value, 299);
#pragma warning disable KMEXP00 // by design
        var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, 4000);

        foreach (var paragraph in paragraphs)
        {
            var result = await kernel.InvokeAsync(qaFunction, new KernelArguments()
            {
                {
                    "input", paragraph
                }
            });

            yield return result.GetValue<string>();
        }
    }

    private static bool IsVision(string model)
    {
        if (model.Contains("vision") || model.Contains("image"))
        {
            return true;
        }

        return false;
    }
}