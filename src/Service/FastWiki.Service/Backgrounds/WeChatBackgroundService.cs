using System.Text;
using System.Threading.Channels;
using FastWiki.Service.Contracts.OpenAI;
using FastWiki.Service.Contracts.WeChat;
using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Infrastructure.Helper;
using FastWiki.Service.Service;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using MemoryService = mem0.NET.Services.MemoryService;

namespace FastWiki.Service.Backgrounds;

public class WeChatBackgroundService(
    ILogger<WeChatBackgroundService> logger,
    IServiceProvider serviceProvider,
    WikiMemoryService wikiMemoryService,
    IMemoryCache memoryCache) : BackgroundService
{
    public static readonly Channel<WeChatAI> Channel = System.Threading.Channels.Channel.CreateUnbounded<WeChatAI>();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        var memoryService =
            scope.ServiceProvider.GetRequiredService<MemoryService>();

        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
        var openAiService = scope.ServiceProvider.GetRequiredService<OpenAIService>();
        var wikiRepository = scope.ServiceProvider.GetRequiredService<IWikiRepository>();
        var fileStorageRepository = scope.ServiceProvider.GetRequiredService<IFileStorageRepository>();

        while (await Channel.Reader.WaitToReadAsync(stoppingToken))
        {
            var content = await Channel.Reader.ReadAsync(stoppingToken);

            await SendMessageAsync(content, memoryService, eventBus, openAiService, wikiRepository,
                fileStorageRepository);
        }
    }


    /// <summary>
    ///     微信AI对话
    /// </summary>
    /// <param name="chatAi"></param>
    /// <param name="memoryService"></param>
    /// <param name="eventBus"></param>
    /// <param name="openAiService"></param>
    /// <param name="wikiRepository"></param>
    /// <param name="fileStorageRepository"></param>
    public async Task SendMessageAsync(WeChatAI chatAi, MemoryService memoryService, IEventBus eventBus,
        OpenAIService openAiService, IWikiRepository wikiRepository, IFileStorageRepository fileStorageRepository)
    {
        var output = new StringBuilder();

        try
        {
            var chatShareInfoQuery = new ChatShareInfoQuery(chatAi.SharedId);

            await eventBus.PublishAsync(chatShareInfoQuery);

            // 如果chatShareId不存在则返回让下面扣款
            var chatShare = chatShareInfoQuery.Result;

            var chatApplicationQuery = new ChatApplicationInfoQuery(chatShareInfoQuery.Result.ChatApplicationId);

            await eventBus.PublishAsync(chatApplicationQuery);

            var chatApplication = chatApplicationQuery?.Result;

            if (chatApplication == null) return;

            var requestToken = 0;

            var module = new ChatCompletionDto<ChatCompletionRequestMessage>
            {
                messages =
                [
                    new ChatCompletionRequestMessage
                    {
                        content = chatAi.Content,
                        role = "user"
                    }
                ]
            };

            var chatHistory = new ChatHistory();

            // 如果设置了Prompt，则添加
            if (!chatApplication.Prompt.IsNullOrEmpty()) chatHistory.AddSystemMessage(chatApplication.Prompt);

            // 保存对话提问
            var createChatRecordCommand = new CreateChatRecordCommand(chatApplication.Id, chatAi.Content);

            await eventBus.PublishAsync(createChatRecordCommand);

            var sourceFile = new List<FileStorage>();
            // 如果为空则不使用知识库
            if (chatApplication.WikiIds.Count != 0)
            {
                var success = await OpenAIService.WikiPrompt(chatApplication, wikiMemoryService, chatAi.Content,
                    fileStorageRepository,
                    wikiRepository,
                    sourceFile, module, null, memoryService);

                if (!success) return;
            }


            // 添加用户输入，并且计算请求token数量
            module.messages.ForEach(x =>
            {
                if (x.content.IsNullOrEmpty()) return;
                requestToken += TokenHelper.ComputeToken(x.content);

                chatHistory.Add(new ChatMessageContent(new AuthorRole(x.role), x.content));
            });


            if (chatShare != null)
            {
                // 如果token不足则返回，使用token和当前request总和大于可用token，则返回
                if (chatShare.AvailableToken != -1 &&
                    chatShare.UsedToken + requestToken >=
                    chatShare.AvailableToken)
                {
                    output.Append("Token不足");
                    return;
                }

                // 如果没有过期则继续
                if (chatShare.Expires != null &&
                    chatShare.Expires < DateTimeOffset.Now)
                {
                    output.Append("Token已过期");
                    return;
                }
            }

            await foreach (var item in openAiService.SendChatMessageAsync(chatApplication,
                               chatHistory))
            {
                if (string.IsNullOrEmpty(item)) continue;

                output.Append(item);
            }

            //对于对话扣款
            if (chatShare != null)
            {
                var updateChatShareCommand = new DeductTokenCommand(chatShare.Id,
                    requestToken);

                await eventBus.PublishAsync(updateChatShareCommand);
            }
        }
        catch (InvalidOperationException invalidOperationException)
        {
            output.Clear();
            output.Append("对话异常:" + invalidOperationException.Message);
            logger.LogError(invalidOperationException, "对话异常");
        }
        catch (ArgumentException argumentException)
        {
            output.Clear();
            output.Append("对话异常:" + argumentException.Message);
            logger.LogError(argumentException, "对话异常");
        }
        catch (Exception e)
        {
            output.Clear();
            output.Append("对话异常,请联系管理员");
            logger.LogError(e, "对话异常");
        }

        var content = output.ToString();

        if (content.IsNullOrEmpty())
        {
            memoryCache.Set(chatAi.MessageId, "抱歉，似乎出现了问题，产生空的消息内容，请联系管理员", TimeSpan.FromMinutes(15));
            return;
        }

        // 写入缓存,15分钟过期
        memoryCache.Set(chatAi.MessageId, content, TimeSpan.FromMinutes(15));
    }
}