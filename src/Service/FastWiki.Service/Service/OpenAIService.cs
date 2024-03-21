using System.Text;
using System.Text.Json;
using FastWiki.Service.Application.Storage.Queries;
using FastWiki.Service.Contracts.OpenAI;
using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Infrastructure;
using FastWiki.Service.Infrastructure.Helper;
using Microsoft.SemanticKernel.ChatCompletion;

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


        if (chatDialogId.IsNullOrEmpty())
        {
            await context.WriteEndAsync(nameof(chatDialogId) + "不能为空");
            return;
        }

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
        // 如果为空则不使用知识库
        if (chatApplication.WikiIds.Count != 0)
        {
            var memoryServerless = context.RequestServices.GetRequiredService<WikiMemoryService>().CreateMemoryServerless();

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
        }

        // 删除最后一个消息
        module.messages.RemoveAt(module.messages.Count - 1);
        module.messages.Add(new ChatCompletionRequestMessage()
        {
            content = prompt,
            role = "user"
        });

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

        var wikiMemoryService = context.RequestServices.GetRequiredService<WikiMemoryService>();

        var chatStream = wikiMemoryService.CreateOpenAIChatCompletionService(chatApplication.ChatModel);

        var output = new StringBuilder();
        await foreach (var item in chatStream.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            if (item.Content.IsNullOrEmpty())
            {
                continue;
            }

            output.Append(item.Content);
            await context.WriteOpenAiResultAsync(item.Content, module.model, Guid.NewGuid().ToString("N"),
                Guid.NewGuid().ToString("N"));
        }

        await context.WriteEndAsync();

        #region 记录对话内容

        var createChatDialogHistoryCommand = new CreateChatDialogHistoryCommand(new CreateChatDialogHistoryInput()
        {
            ChatDialogId = chatDialogId,
            Content = question,
            ExpendToken = requestToken,
            Type = ChatDialogHistoryType.Text,
            Current = true
        });

        await eventBus.PublishAsync(createChatDialogHistoryCommand);

        var outputContent = output.ToString();

        var chatDialogHistory = new CreateChatDialogHistoryCommand(new CreateChatDialogHistoryInput()
        {
            ChatDialogId = chatDialogId,
            Content = outputContent,
            ExpendToken = TokenHelper.ComputeToken(outputContent),
            Type = ChatDialogHistoryType.Text,
            Current = false,
            SourceFile = sourceFile.Select(x => new SourceFileDto()
            {
                Name = x.Name,
                FileId = x.Id.ToString(),
                FilePath = x.Path
            }).ToList()
        });

        await eventBus.PublishAsync(chatDialogHistory);

        #endregion
        
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