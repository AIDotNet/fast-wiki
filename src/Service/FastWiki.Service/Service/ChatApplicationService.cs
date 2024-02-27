using Microsoft.SemanticKernel.ChatCompletion;

namespace FastWiki.Service.Service;

/// <inheritdoc />
public sealed class ChatApplicationService(WikiMemoryService wikiMemoryService)
    : ApplicationService<ChatApplicationService>, IChatApplicationService
{
    /// <inheritdoc />
    public async Task CreateAsync(CreateChatApplicationInput input)
    {
        var command = new CreateChatApplicationCommand(input);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    public async Task RemoveAsync(string id)
    {
        var command = new RemoveChatApplicationCommand(id);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(UpdateChatApplicationInput input)
    {
        var command = new UpdateChatApplicationCommand(input);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    public async Task<PaginatedListBase<ChatApplicationDto>> GetListAsync(int page, int pageSize)
    {
        var query = new ChatApplicationQuery(page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <inheritdoc />
    public async Task<ChatApplicationDto> GetAsync(string id)
    {
        var query = new ChatApplicationInfoQuery(id);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <inheritdoc />
    public async Task CreateChatDialogAsync(CreateChatDialogInput input)
    {
        var command = new CreateChatDialogCommand(input);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    public async Task<List<ChatDialogDto>> GetChatDialogAsync()
    {
        var query = new ChatDialogQuery();

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<CompletionsDto> CompletionsAsync(CompletionsInput input)
    {
        var chatApplicationQuery = new ChatApplicationInfoQuery(input.ChatApplicationId);

        await EventBus.PublishAsync(chatApplicationQuery);

        if (chatApplicationQuery.Result == null)
        {
            throw new UserFriendlyException("应用Id不存在");
        }

        var prompt = string.Empty;

        // 如果为空则不使用知识库
        if (chatApplicationQuery.Result.WikiIds.Count != 0)
        {
            var memoryServerless = GetRequiredService<MemoryServerless>();

            var filters = chatApplicationQuery.Result.WikiIds
                .Select(chatApplication => new MemoryFilter().ByTag("wikiId", chatApplication.ToString())).ToList();


            var result = await memoryServerless.SearchAsync(input.Content, "wiki", filters: filters, limit: 3);

            result.Results.ForEach(x =>
            {
                prompt += string.Join(Environment.NewLine, x.Partitions.Select(x => x.Text));
            });
        }

        var chatStream = wikiMemoryService.CreateOpenAIChatCompletionService(chatApplicationQuery.Result.ChatModel);

        var chatHistory = new ChatHistory();

        if (!chatApplicationQuery.Result.Prompt.IsNullOrWhiteSpace())
        {
            chatHistory.AddSystemMessage(chatApplicationQuery.Result.Prompt);
        }

        // TODO: 后期可修改为可配置
        var historyQuery = new ChatDialogHistoryQuery(input.ChatDialogId, 1, 3);

        await EventBus.PublishAsync(historyQuery);

        foreach (var message in historyQuery.Result.Result)
        {
            if (message.Current)
            {
                chatHistory.AddUserMessage(message.Content);
            }
            else
            {
                chatHistory.AddAssistantMessage(message.Content);
            }
        }

        chatHistory.AddUserMessage(chatApplicationQuery.Result.Template.Replace("{{quote}}", prompt)
            .Replace("{{question}}", input.Content));

        await foreach (var item in chatStream.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            yield return new CompletionsDto()
            {
                Content = item.Content ?? string.Empty
            };
            await Task.Delay(1);
        }
    }

    public async Task CreateChatDialogHistoryAsync(CreateChatDialogHistoryInput input)
    {
        var command = new CreateChatDialogHistoryCommand(input);

        await EventBus.PublishAsync(command);
    }

    public async Task<PaginatedListBase<ChatDialogHistoryDto>> GetChatDialogHistoryAsync(string chatDialogId, int page,
        int pageSize)
    {
        var query = new ChatDialogHistoryQuery(chatDialogId, page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    public async Task RemoveDialogHistoryAsync(string id)
    {
        var command = new RemoveChatDialogHistoryCommand(id);

        await EventBus.PublishAsync(command);
    }
}