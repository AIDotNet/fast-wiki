namespace FastWiki.Service.Service;

/// <inheritdoc />
public sealed class ChatApplicationService(
    IChatApplicationRepository chatApplicationRepository,
    IMapper mapper)
    : ApplicationService<ChatApplicationService>
{
    [IgnoreRoute]
    public async Task<ChatShareDto> GetAPIKeyChatShareAsync(string apiKey)
    {
        return mapper.Map<ChatShareDto>(await chatApplicationRepository.GetAPIKeyChatShareAsync(apiKey));
    }

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
        var query = new ChatShareQuery(chatApplicationId, page, pageSize, UserContext.GetUserId<Guid>());

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
    public async Task CreateQuestionsAsync(QuestionsInput input)
    {
        var command = new CreateQuestionsCommand(input);

        await EventBus.PublishAsync(command);
    }

    public async Task<List<QuestionsDto>> GetQuestionsAsync(string applicationId)
    {
        var query = new GetQuestionsQuery(applicationId);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    public async Task<List<QuestionsDto>> GetSharedQuestionsAsync(string sharedId)
    {
        var chatShareInfoQuery = new ChatShareInfoQuery(sharedId);

        await EventBus.PublishAsync(chatShareInfoQuery);
        var query = new GetQuestionsQuery(chatShareInfoQuery.Result.ChatApplicationId);

        await EventBus.PublishAsync(query);

        return query.Result;
    }


    [Authorize]
    public Task RemoveQuestionsAsync(string id)
    {
        var command = new RemoveQuestionsCommand(id);

        return EventBus.PublishAsync(command);
    }
}