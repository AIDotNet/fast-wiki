namespace FastWiki.Service.Service;

/// <inheritdoc />
public class ChatApplicationService : ApplicationService<ChatApplicationService>, IChatApplicationService
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
}