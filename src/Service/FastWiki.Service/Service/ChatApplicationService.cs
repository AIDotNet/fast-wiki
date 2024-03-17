using Microsoft.Extensions.Caching.Memory;

namespace FastWiki.Service.Service;

/// <inheritdoc />
public sealed class ChatApplicationService(WikiMemoryService wikiMemoryService, IMemoryCache memoryCache)
    : ApplicationService<ChatApplicationService>, IChatApplicationService
{
    /// <inheritdoc />
    [Authorize]
    public async Task CreateAsync(CreateChatApplicationInput input)
    {
        var command = new CreateChatApplicationCommand(input);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    [Authorize]
    public async Task RemoveAsync(string id)
    {
        var command = new RemoveChatApplicationCommand(id);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    [Authorize]
    public async Task UpdateAsync(UpdateChatApplicationInput input)
    {
        var command = new UpdateChatApplicationCommand(input);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    [Authorize]
    public async Task<PaginatedListBase<ChatApplicationDto>> GetListAsync(int page, int pageSize)
    {
        var query = new ChatApplicationQuery(page, pageSize, UserContext.GetUserId<Guid>());

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <inheritdoc />
    [Authorize]
    public async Task<ChatApplicationDto> GetAsync(string id)
    {
        var query = new ChatApplicationInfoQuery(id);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    public async Task<ChatApplicationDto> GetChatShareApplicationAsync(string chatShareId)
    {
        var query = new ChatShareApplicationQuery(chatShareId);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <inheritdoc />
    public async Task CreateChatDialogAsync(CreateChatDialogInput input)
    {
        var command = new CreateChatDialogCommand(input);

        await EventBus.PublishAsync(command);
    }

    /// <param name="applicationId"></param>
    /// <param name="all"></param>
    /// <inheritdoc />
    [Authorize]
    public async Task<List<ChatDialogDto>> GetChatDialogAsync(string applicationId, bool all)
    {
        var query = new ChatDialogQuery(applicationId, all,UserContext.GetUserId<Guid>());

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <param name="chatId"></param>
    /// <inheritdoc />
    public async Task<List<ChatDialogDto>> GetChatShareDialogAsync(string chatId)
    {
        var query = new ChatShareDialogQuery(chatId);

        await EventBus.PublishAsync(query);

        return query.Result;
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

    [Authorize]
    public async Task CreateShareAsync(CreateChatShareInput input)
    {
        var command = new CreateChatShareCommand(input);

        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task<PaginatedListBase<ChatShareDto>> GetChatShareListAsync(string chatApplicationId, int page,
        int pageSize)
    {
        var query = new ChatShareQuery(chatApplicationId, page, pageSize,UserContext.GetUserId<Guid>());

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    [Authorize]
    public Task RemoveChatShareAsync(string id)
    {
        var command = new RemoveChatShareCommand(id);

        return EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task RemoveDialogAsync(string id)
    {
        var command = new RemoveChatDialogCommand(id);

        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task UpdateDialogAsync(ChatDialogDto input)
    {
        var command = new UpdateChatDialogCommand(input);

        await EventBus.PublishAsync(command);
    }

    public async Task RemoveShareDialogAsync(string chatId, string id)
    {
        var command = new RemoveShareDialogCommand(chatId, id);
        await EventBus.PublishAsync(command);
    }

    public async Task UpdateShareDialogAsync(ChatDialogDto input)
    {
        var command = new UpdateShareChatDialogCommand(input);

        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task<PaginatedListBase<ChatDialogDto>> GetSessionLogDialogAsync(string chatApplicationId, int page,
        int pageSize)
    {
        var query = new GetSessionLogDialogQuery(chatApplicationId, page, pageSize,UserContext.GetUserId<Guid>());

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    public Task PutChatHistoryAsync(PutChatHistoryInput input)
    {
        var command = new PutChatHistoryCommand(input);

        return EventBus.PublishAsync(command);
    }
}