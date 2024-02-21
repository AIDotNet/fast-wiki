namespace FastWiki.Service.Service;

/// <summary>
/// 知识库服务
/// </summary>
public sealed class WikiService : ApplicationService<WikiService>, IWikiService
{
    /// <inheritdoc />
    public async Task CreateAsync(CreateWikiInput input)
    {
        var command = new CreateWikiCommand(input);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    public async Task<WikiDto> GetAsync(long id)
    {
        var query = new WikiQuery(id);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <inheritdoc />
    public async Task<PaginatedListBase<WikiDto>> GetWikiListAsync(string? keyword, int page, int pageSize)
    {
        var query = new WikiListQuery(keyword, page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <inheritdoc />
    public async Task RemoveAsync(long id)
    {
        var command = new RemoveWikiCommand(id);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    public async Task CreateWikiDetailsAsync(CreateWikiDetailsInput input)
    {
        var command = new CreateWikiDetailsCommand(input);

        await EventBus.PublishAsync(command);
    }

    public async Task<PaginatedListBase<WikiDetailDto>> GetWikiDetailsAsync(long wikiId, string? keyword, int page, int pageSize)
    {
        var query = new WikiDetailsQuery(wikiId, keyword, page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }
}