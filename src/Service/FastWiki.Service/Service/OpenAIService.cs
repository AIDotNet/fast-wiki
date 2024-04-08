using System.Text;
using System.Text.Json;
using AIDotNet.OpenAI;
using Azure.AI.OpenAI;
using FastWiki.Service.Application.Function.Queries;
using FastWiki.Service.Application.Model.Commands;
using FastWiki.Service.Application.Storage.Queries;
using FastWiki.Service.Contracts.OpenAI;
using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Infrastructure;
using FastWiki.Service.Infrastructure.Helper;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using TokenApi.Service.Exceptions;
using Constant = AIDotNet.Abstractions.Constant;

namespace FastWiki.Service.Service;

public static class OpenAIService
{
    public static async Task Completions(HttpContext context)
    {
        using var stream = new StreamReader(context.Request.Body);

        var module =
            JsonSerializer.Deserialize<ChatCompletionDto<ChatCompletionRequestMessage>>(await stream.ReadToEndAsync());

        context.Response.ContentType = "text/event-stream";

        if (module == null)
        {
            await context.WriteEndAsync("Invalid request");

            return;
        }

        var chatDialogId = context.Request.Query["ChatDialogId"];
        var chatId = context.Request.Query["ChatId"];
        var token = context.Request.Headers.Authorization;
        var chatShareId = context.Request.Query["ChatShareId"];

        if (chatId.IsNullOrEmpty())
        {
            await context.WriteEndAsync(nameof(chatId) + "不能为空");
            return;
        }

        var eventBus = context.RequestServices.GetRequiredService<IEventBus>();

        var getAPIKeyChatShareQuery = new GetAPIKeyChatShareQuery(token);

        // 如果是sk-开头的token则是应用token
        if (chatShareId.IsNullOrEmpty() && !token.ToString().Replace("Bearer ", "").Trim().StartsWith("sk-"))
        {
            // 判断当前用户是否登录
            if (context.User.Identity?.IsAuthenticated == false)
            {
                context.Response.StatusCode = 401;
                await context.WriteEndAsync("Token不能为空");
                return;
            }
        }
        else if (chatShareId.IsNullOrEmpty())
        {
            await eventBus.PublishAsync(getAPIKeyChatShareQuery);

            if (getAPIKeyChatShareQuery.Result == null)
            {
                context.Response.StatusCode = 401;
                await context.WriteEndAsync("Token无效");
                return;
            }
        }

        ChatApplicationDto chatApplication = null;

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
        else
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

        int requestToken = 0;

        var chatHistory = new ChatHistory();

        // 如果设置了Prompt，则添加
        if (!chatApplication.Prompt.IsNullOrEmpty())
        {
            chatHistory.AddSystemMessage(chatApplication.Prompt);
        }


        var content = module.messages.Last();
        var question = content.content;

        var prompt = string.Empty;

        var sourceFile = new List<FileStorage>();
        var wikiMemoryService = context.RequestServices.GetRequiredService<WikiMemoryService>();
        var memoryServerless = wikiMemoryService.CreateMemoryServerless();

        // 如果为空则不使用知识库
        if (chatApplication.WikiIds.Count != 0)
        {
            var filters = chatApplication.WikiIds
                .Select(chatApplication => new MemoryFilter().ByTag("wikiId", chatApplication.ToString())).ToList();

            var result = await memoryServerless.SearchAsync(content.content, "wiki", filters: filters, limit: 3,
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
                    .Replace("{{question}}", content.content);
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
        }

        // 添加用户输入，并且计算请求token数量
        module.messages.ForEach(x =>
        {
            if (!x.content.IsNullOrEmpty())
            {
                requestToken += TokenHelper.ComputeToken(x.content);
                if (x.role == "user")
                {
                    chatHistory.AddUserMessage(x.content);
                }
                else if (x.role == "assistant")
                {
                    chatHistory.AddSystemMessage(x.content);
                }
                else if (x.role == "system")
                {
                    chatHistory.AddSystemMessage(x.content);
                }
            }
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

        if (chatApplication.ChatType.IsNullOrEmpty())
        {
            // 防止没有设置对话类型
            chatApplication.ChatType = OpenAIOptions.ServiceName;
        }

        var modelService = context.RequestServices.GetRequiredService<ModelService>();

        var (chatStream, fastModelDto) = await modelService.GetChatService(chatApplication.ChatType);

        if (fastModelDto.Enable != true)
        {
            await context.WriteEndAsync("模型未启用");
            return;
        }

        if (fastModelDto.Models.Any(x => x == chatApplication.ChatModel) == false)
        {
            await context.WriteEndAsync($"模型渠道并未找到 {chatApplication.ChatModel} 模型的支持！");
            return;
        }

        var setting = new OpenAIPromptExecutionSettings
        {
            MaxTokens = chatApplication.MaxResponseToken,
            Temperature = chatApplication.Temperature,
            ModelId = chatApplication.ChatModel,
            ExtensionData = new Dictionary<string, object>()
        };

        setting.ExtensionData.TryAdd(Constant.API_KEY, fastModelDto.ApiKey);
        setting.ExtensionData.TryAdd(Constant.API_URL, fastModelDto.Url);

        // 判断apikey和url是否为空

        if (fastModelDto.ApiKey.IsNullOrEmpty() || fastModelDto.Url.IsNullOrEmpty())
        {
            await context.WriteEndAsync("API Key 或 Url 不能为空");
            return;
        }

        var responseId = Guid.NewGuid().ToString("N");
        var requestId = Guid.NewGuid().ToString("N");
        var output = new StringBuilder();
        try
        {
            var functionCall = new ChatApplicationFunctionCallQuery(chatApplication.FunctionIds.ToArray());

            if (chatApplication.FunctionIds.Any())
            {
                await eventBus.PublishAsync(functionCall);
            }

            // 如果有函数调用
            if (chatApplication.FunctionIds.Any() && functionCall.Result.Any())
            {
                // 只支持OpenAI
                if (fastModelDto.Type != OpenAIOptions.ServiceName)
                {
                    await context.WriteEndAsync("Function Call目前仅支持OpenAI模型");
                    return;
                }

                var kernel = wikiMemoryService.CreateFunctionKernel(functionCall.Result.ToList(), fastModelDto.ApiKey,
                    chatApplication.ChatModel, fastModelDto.Url);

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
                    // TODO: 如果这里跳过则会进入对话？
                    await context.WriteEndAsync("未找到函数");
                    return;
                }

                foreach (var toolCall in toolCalls)
                {
                    kernel.Plugins.TryGetFunctionAndArguments(toolCall, out var function,
                        out KernelArguments? arguments);

                    if (function == null)
                    {
                        // TODO: 是否需要跳过？
                        continue;
                    }

                    try
                    {
                        var functionResult = await function.InvokeAsync(kernel, new KernelArguments()
                        {
                            {
                                "value", arguments?.Select(x => x.Value).ToArray()
                            }
                        });

                        if (functionResult.ValueType == null)
                        {
                            continue;
                        }
                        
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
                        await context.WriteEndAsync("函数调用异常：" + e.Message);
                        return;
                    }
                }

                await foreach (var item in chat.GetStreamingChatMessageContentsAsync(chatHistory, setting))
                {
                    if (item.Content.IsNullOrEmpty())
                    {
                        continue;
                    }

                    output.Append(item.Content);
                    await context.WriteOpenAiResultAsync(item.Content, module.model, requestId,
                        responseId);
                }
            }
            else
            {
                await foreach (var item in chatStream.GetStreamingChatMessageContentsAsync(chatHistory, setting))
                {
                    if (item.Content.IsNullOrEmpty())
                    {
                        continue;
                    }

                    output.Append(item.Content);
                    await context.WriteOpenAiResultAsync(item.Content, module.model, requestId,
                        responseId);
                }
            }
        }
        catch (NotModelException notModelException)
        {
            await context.WriteEndAsync("未找到模型兼容：" + notModelException.Message);
            return;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            await context.WriteEndAsync("对话异常：" + invalidOperationException.Message);
            return;
        }
        catch (ArgumentException argumentException)
        {
            await context.WriteEndAsync(argumentException.Message);
            return;
        }
        catch (Exception e)
        {
            await context.WriteEndAsync("对话异常：" + e.Message);
            return;
        }

        await context.WriteEndAsync();

        #region 记录对话内容

        if (!string.IsNullOrEmpty(chatDialogId))
        {
            var createChatDialogHistoryCommand = new CreateChatDialogHistoryCommand(new CreateChatDialogHistoryInput()
            {
                ChatDialogId = chatDialogId!,
                Id = requestId,
                Content = question,
                ExpendToken = requestToken,
                Type = ChatDialogHistoryType.Text,
                Current = true
            });

            await eventBus.PublishAsync(createChatDialogHistoryCommand);
        }


        var outputContent = output.ToString();
        var completeToken = TokenHelper.ComputeToken(outputContent);

        if (!string.IsNullOrEmpty(chatDialogId))
        {
            var chatDialogHistory = new CreateChatDialogHistoryCommand(new CreateChatDialogHistoryInput()
            {
                ChatDialogId = chatDialogId!,
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
        }

        #endregion

        var fastModelComputeTokenCommand = new FastModelComputeTokenCommand(chatApplication.ChatType, requestToken,
            completeToken);

        await eventBus.PublishAsync(fastModelComputeTokenCommand);

        //对于对话扣款
        if (getAPIKeyChatShareQuery?.Result != null)
        {
            var updateChatShareCommand = new DeductTokenCommand(getAPIKeyChatShareQuery.Result.Id,
                requestToken);

            await eventBus.PublishAsync(updateChatShareCommand);
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