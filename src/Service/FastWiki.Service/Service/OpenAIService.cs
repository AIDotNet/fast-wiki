using System.Text;
using System.Text.Json;
using Azure.AI.OpenAI;
using FastWiki.Service.Contracts.OpenAI;
using FastWiki.Service.Domain.Function.Aggregates;
using FastWiki.Service.Domain.Function.Repositories;
using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Infrastructure;
using FastWiki.Service.Infrastructure.Helper;
using mem0.Core.Model;
using Microsoft.KernelMemory.DataFormats.Text;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using MemoryService = mem0.NET.Services.MemoryService;

namespace FastWiki.Service.Service;

/// <summary>
/// OpenAI服务
/// </summary>
/// <param name="logger"></param>
public class OpenAIService(
    ILogger<OpenAIService> logger,
    IEventBus eventBus,
    IFastWikiFunctionCallRepository fastWikiFunctionCallRepository,
    IFileStorageRepository fileStorageRepository,
    IWikiRepository wikiRepository,
    WikiMemoryService wikiMemoryService)
{
    /// <summary>
    ///     ChatCompletion
    /// </summary>
    /// <param name="context"></param>
    /// <param name="input"></param>
    /// <param name="chatApplicationService"></param>
    public async Task Completions(HttpContext context, ChatCompletionDto<ChatCompletionRequestMessage> input,
        ChatApplicationService chatApplicationService)
    {
        context.Response.ContentType = "text/event-stream";

        var chatId = context.Request.Query["ChatId"].ToString();
        var token = context.Request.Headers.Authorization;

        if (chatId.IsNullOrEmpty()) chatId = input.ApplicationId;

        if (chatId.IsNullOrEmpty()) chatId = context.Request.Headers["applicationId"];

        // 获取分享Id
        var chatShareId = context.Request.Query["ChatShareId"].ToString();
        if (chatShareId.IsNullOrEmpty()) chatShareId = input.SharedId;

        if (chatShareId.IsNullOrEmpty()) chatShareId = context.Request.Headers["sharedId"];

        ChatApplicationDto chatApplication = null;

        ChatShareDto apiKeyChatShare = null;

        if (token.ToString().Replace("Bearer ", "").Trim().StartsWith("sk-"))
        {
            apiKeyChatShare = await
                chatApplicationService.GetAPIKeyChatShareAsync(token.ToString().Replace("Bearer ", "").Trim());
            if (apiKeyChatShare == null || apiKeyChatShare.ChatApplicationId.IsNullOrEmpty())
            {
                context.Response.StatusCode = 401;
                await context.WriteEndAsync("Token无效");
                return;
            }

            var chatApplicationQuery = new ChatApplicationInfoQuery(apiKeyChatShare.ChatApplicationId);

            await eventBus.PublishAsync(chatApplicationQuery);

            chatApplication = chatApplicationQuery?.Result;
        }
        else
        {
            // 如果不是sk则校验用户 并且不是分享链接
            if (chatShareId.IsNullOrEmpty())
                // 判断当前用户是否登录
                if (context.User.Identity?.IsAuthenticated == false)
                {
                    context.Response.StatusCode = 401;
                    await context.WriteEndAsync("Token不能为空");
                    return;
                }

            // 如果是分享链接则获取分享信息
            if (!chatShareId.IsNullOrEmpty())
            {
                var chatShareInfoQuery = new ChatShareInfoQuery(chatShareId);

                await eventBus.PublishAsync(chatShareInfoQuery);

                // 如果chatShareId不存在则返回让下面扣款
                apiKeyChatShare = chatShareInfoQuery.Result;

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

        var requestToken = 0;

        var chatHistory = new ChatHistory();

        // 如果设置了Prompt，则添加
        if (!chatApplication.Prompt.IsNullOrEmpty()) chatHistory.AddSystemMessage(chatApplication.Prompt);


        var content = input.messages.Last();
        var question = content.content;

        // 保存对话提问
        var createChatRecordCommand = new CreateChatRecordCommand(chatApplication.Id, question);

        await eventBus.PublishAsync(createChatRecordCommand);

        var sourceFile = new List<FileStorage>();

        // 如果为空则不使用知识库
        if (chatApplication.WikiIds.Count != 0)
        {
            var success = await WikiPrompt(chatApplication, wikiMemoryService, content.content,
                fileStorageRepository,
                wikiRepository,
                sourceFile, input, async x => { await context.WriteEndAsync(x); },
                context.RequestServices.GetRequiredService<mem0.NET.Services.MemoryService>());

            if (!success) return;
        }

        // 添加用户输入，并且计算请求token数量
        input.messages.ForEach(x =>
        {
            if (x.content.IsNullOrEmpty()) return;
            requestToken += TokenHelper.ComputeToken(x.content);

            chatHistory.Add(new ChatMessageContent(new AuthorRole(x.role), x.content));
        });


        if (apiKeyChatShare != null)
        {
            // 如果token不足则返回，使用token和当前request总和大于可用token，则返回
            if (apiKeyChatShare.AvailableToken != -1 &&
                apiKeyChatShare.UsedToken + requestToken >=
                apiKeyChatShare.AvailableToken)
            {
                await context.WriteEndAsync("您的Token不足");
                return;
            }

            // 如果没有过期则继续
            if (apiKeyChatShare.Expires != null &&
                apiKeyChatShare.Expires < DateTimeOffset.Now)
            {
                await context.WriteEndAsync("您的Token已过期");
                return;
            }
        }


        var responseId = Guid.NewGuid().ToString("N");
        var requestId = Guid.NewGuid().ToString("N");
        var output = new StringBuilder();
        try
        {
            await foreach (var item in SendChatMessageAsync(chatApplication, chatHistory))
            {
                if (string.IsNullOrEmpty(item)) continue;

                output.Append(item);
                await context.WriteOpenAiResultAsync(item, input.model, requestId,
                    responseId);
            }
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
        if (apiKeyChatShare != null)
        {
            var updateChatShareCommand = new DeductTokenCommand(apiKeyChatShare.Id,
                requestToken);

            await eventBus.PublishAsync(updateChatShareCommand);
        }
    }

    /// <summary>
    ///     提问AI
    /// </summary>
    /// <param name="chatApplication"></param>
    /// <param name="chatHistory"></param>
    /// <returns></returns>
    public async IAsyncEnumerable<string> SendChatMessageAsync(ChatApplicationDto chatApplication,
        ChatHistory chatHistory)
    {
        var functionCall = new List<FastWikiFunctionCall>();

        if (chatApplication.FunctionIds.Any())
            functionCall.AddRange(
                await fastWikiFunctionCallRepository.GetListAsync(x => chatApplication.FunctionIds.Contains(x.Id)));

        var kernel =
            wikiMemoryService.CreateFunctionKernel(functionCall.ToList(), chatApplication.ChatModel);

        var chat = kernel.GetRequiredService<IChatCompletionService>();

        // 如果有函数调用
        if (chatApplication.FunctionIds.Count != 0 && functionCall.Count > 0)
        {
            OpenAIPromptExecutionSettings settings = new()
            {
                ToolCallBehavior = ToolCallBehavior.EnableKernelFunctions
            };

            var result =
                (OpenAIChatMessageContent)await chat.GetChatMessageContentAsync(chatHistory, settings,
                    kernel);


            var toolCalls =
                result.ToolCalls.OfType<ChatCompletionsFunctionToolCall>().ToList();

            if (toolCalls.Count == 0)
            {
                yield return "未找到函数";
                yield break;
            }

            foreach (var toolCall in toolCalls)
            {
                kernel.Plugins.TryGetFunctionAndArguments(toolCall, out var function,
                    out var arguments);

                if (function == null) continue;

                Exception? exception = null;

                try
                {
                    var functionResult = await function.InvokeAsync(kernel, new KernelArguments
                    {
                        {
                            "value", arguments?.Select(x => x.Value).ToArray()
                        }
                    });
                    // 判断ValueType是否为值类型
                    if (functionResult.ValueType?.IsValueType == true || functionResult.ValueType == typeof(string))
                        chatHistory.AddAssistantMessage(functionResult.GetValue<object>().ToString());
                    else
                        // 记录函数调用
                        chatHistory.AddAssistantMessage(
                            JsonSerializer.Serialize(functionResult.GetValue<object>()));
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
                if (string.IsNullOrEmpty(message)) continue;

                yield return message;
            }
        }


        await foreach (var item in chat.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            var message = item.Content;
            if (string.IsNullOrEmpty(message)) continue;

            yield return message;
        }
    }

    public static async IAsyncEnumerable<string> SendChatMessageAsync(IChatCompletionService chat,
        ChatHistory chatHistory)
    {
        await foreach (var item in chat.GetStreamingChatMessageContentsAsync(chatHistory).ConfigureAwait(false))
            yield return item.Content;
    }

    /// <summary>
    ///     知识库Prompt组合
    ///     在向量中搜索响应的知识库内容，然后将其添加到对话中
    /// </summary>
    /// <param name="chatApplication"></param>
    /// <param name="wikiMemoryService"></param>
    /// <param name="content"></param>
    /// <param name="fileStorageRepository"></param>
    /// <param name="wikiRepository"></param>
    /// <param name="sourceFile"></param>
    /// <param name="module"></param>
    /// <param name="notFoundAction"></param>
    /// <param name="requiredService"></param>
    /// <returns></returns>
    public static async ValueTask<bool> WikiPrompt(ChatApplicationDto chatApplication,
        WikiMemoryService wikiMemoryService, string content,
        IFileStorageRepository fileStorageRepository,
        IWikiRepository wikiRepository,
        List<FileStorage> sourceFile,
        ChatCompletionDto<ChatCompletionRequestMessage> module,
        Func<string, ValueTask>? notFoundAction, MemoryService requiredService)
    {
        var prompt = string.Empty;
        var filters = chatApplication.WikiIds
            .Select(chatApplication => new MemoryFilter().ByTag("wikiId", chatApplication.ToString())).ToList();

        var wikis = await wikiRepository.GetListAsync(x => chatApplication.WikiIds.Contains(x.Id));


        foreach (var wiki in wikis)
        {
            if (wiki.VectorType == VectorType.Mem0)
            {
                var fileIds = new List<long>();
                var values = await requiredService.SearchMemory(content, null, wiki.Id.ToString(), null, 3);

                values = values.Where(x => x.Score > chatApplication.Relevancy).ToList();

                values.ForEach(x =>
                {
                    // 如果使用metaData那么可能会导致MaxToken超出限制。
                    var metaData = x.MetaData["metaData"];
                    prompt += metaData + Environment.NewLine;
                });
                if (values.Count == 0 && !string.IsNullOrWhiteSpace(chatApplication.NoReplyFoundTemplate))
                {
                    if (notFoundAction != null)
                    {
                        await notFoundAction.Invoke(chatApplication.NoReplyFoundTemplate);
                    }

                    return false;
                }

                var tokens = TokenHelper.GetGptEncoding().Encode(prompt);

                // 这里可以有效的防止token数量超出限制，但是也会降低回复的质量
                prompt = TokenHelper.GetGptEncoding()
                    .Decode(tokens.Take(chatApplication.MaxResponseToken).ToList());

                // 如果prompt不为空，则需要进行模板替换
                if (!prompt.IsNullOrEmpty())
                    prompt = chatApplication.Template.Replace("{{quote}}", prompt)
                        .Replace("{{question}}", content);

                // 在这里需要获取源文件
                if (fileIds.Count > 0 && chatApplication.ShowSourceFile)
                {
                    var fileResult = await fileStorageRepository.GetListAsync(fileIds.ToArray());

                    sourceFile.AddRange(fileResult);
                }

                if (!prompt.IsNullOrEmpty())
                {
                    // 删除最后一个消息
                    module.messages.RemoveAt(module.messages.Count - 1);
                    module.messages.Add(new ChatCompletionRequestMessage
                    {
                        content = prompt,
                        role = "user"
                    });
                }
            }
            else
            {
                var memoryServerless = wikiMemoryService.CreateMemoryServerless(wiki.EmbeddingModel, wiki.Model);

                var result = await memoryServerless.SearchAsync(content, "wiki", filters: filters, limit: 3,
                    minRelevance: chatApplication.Relevancy);

                var fileIds = new List<long>();

                result.Results.ForEach(x =>
                {
                    // 获取fileId
                    var fileId = x.Partitions.Select(x => x.Tags.FirstOrDefault(x => x.Key == "fileId"))
                        .FirstOrDefault(x => !x.Value.IsNullOrEmpty())
                        .Value.FirstOrDefault();

                    if (!fileId.IsNullOrWhiteSpace() && long.TryParse(fileId, out var id)) fileIds.Add(id);

                    prompt += string.Join(Environment.NewLine, x.Partitions.Select(x => x.Text));
                });

                if (result.Results.Count == 0 &&
                    !string.IsNullOrWhiteSpace(chatApplication.NoReplyFoundTemplate))
                {
                    if (notFoundAction != null)
                    {
                        await notFoundAction.Invoke(chatApplication.NoReplyFoundTemplate);
                    }

                    return false;
                }

                var tokens = TokenHelper.GetGptEncoding().Encode(prompt);

                // 这里可以有效的防止token数量超出限制，但是也会降低回复的质量
                prompt = TokenHelper.GetGptEncoding()
                    .Decode(tokens.Take(chatApplication.MaxResponseToken).ToList());

                // 如果prompt不为空，则需要进行模板替换
                if (!prompt.IsNullOrEmpty())
                    prompt = chatApplication.Template.Replace("{{quote}}", prompt)
                        .Replace("{{question}}", content);

                // 在这里需要获取源文件
                if (fileIds.Count > 0 && chatApplication.ShowSourceFile)
                {
                    var fileResult = await fileStorageRepository.GetListAsync(fileIds.ToArray());

                    sourceFile.AddRange(fileResult);
                }

                if (!prompt.IsNullOrEmpty())
                {
                    // 删除最后一个消息
                    module.messages.RemoveAt(module.messages.Count - 1);
                    module.messages.Add(new ChatCompletionRequestMessage
                    {
                        content = prompt,
                        role = "user"
                    });
                }
            }
        }


        return true;
    }

    /// <summary>
    ///     QA问答解析大文本拆分多个段落
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


#pragma warning disable KMEXP00
        var lines = TextChunker.SplitPlainTextLines(value, 299);
#pragma warning restore KMEXP00
#pragma warning disable KMEXP00
        var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, 4000);
#pragma warning restore KMEXP00

        foreach (var paragraph in paragraphs)
        {
            var result = await kernel.InvokeAsync(qaFunction, new KernelArguments
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
        if (model.Contains("vision") || model.Contains("image")) return true;

        return false;
    }
}