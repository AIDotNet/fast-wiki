namespace FastWiki.Service.Service;

/// <summary>
/// 知识库服务
/// </summary>
public sealed class WikiService : ApplicationService<WikiService>, IWikiService
{
    /// <inheritdoc />
    [Authorize]
    public async Task CreateAsync(CreateWikiInput input)
    {
        var command = new CreateWikiCommand(input);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    [Authorize]
    public async Task<WikiDto> GetAsync(long id)
    {
        var query = new WikiQuery(id);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    [Authorize]
    public async Task UpdateAsync(WikiDto dto)
    {
        var command = new UpdateWikiCommand(dto);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    [Authorize]
    public async Task<PaginatedListBase<WikiDto>> GetWikiListAsync(string? keyword, int page, int pageSize)
    {
        var query = new WikiListQuery(UserContext.GetUserId<Guid>(),keyword, page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <inheritdoc />
    [Authorize]
    public async Task RemoveAsync(long id)
    {
        var command = new RemoveWikiCommand(id);

        await EventBus.PublishAsync(command);
    }

    /// <inheritdoc />
    [Authorize]
    public async Task CreateWikiDetailsAsync(CreateWikiDetailsInput input)
    {
        var command = new CreateWikiDetailsCommand(input);

        await EventBus.PublishAsync(command);
    }

    public async Task CreateWikiDetailWebPageInputAsync(CreateWikiDetailWebPageInput input)
    {
        var command = new CreateWikiDetailWebPageCommand(input);

        await EventBus.PublishAsync(command);
    }

    public async Task CreateWikiDetailDataAsync(CreateWikiDetailDataInput input)
    {
        var command = new CreateWikiDetailDataCommand(input);

        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task<PaginatedListBase<WikiDetailDto>> GetWikiDetailsAsync(long wikiId, WikiQuantizationState? state,
        string? keyword, int page, int pageSize)
    {
        var query = new WikiDetailsQuery(wikiId, state, keyword, page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    [Authorize]
    public async Task RemoveDetailsAsync(long id)
    {
        var command = new RemoveWikiDetailsCommand(id);

        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task<PaginatedListBase<WikiDetailVectorQuantityDto>> GetWikiDetailVectorQuantityAsync(
        string wikiDetailId, int page, int pageSize)
    {
        var query = new WikiDetailVectorQuantityQuery(wikiDetailId, page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    [Authorize]
    public async Task RemoveDetailVectorQuantityAsync(string documentId)
    {
        var command = new RemoveWikiDetailVectorQuantityCommand(documentId);

        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task<SearchVectorQuantityResult> GetSearchVectorQuantityAsync(long wikiId, string search,
        double minRelevance = 0D)
    {
        var query = new SearchVectorQuantityQuery(wikiId, search, minRelevance);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    public async Task RemoveDetailsVectorAsync(string id)
    {
        var command = new RemoveDetailsVectorCommand(System.Web.HttpUtility.UrlDecode(id));

        await EventBus.PublishAsync(command);
    }

    public async Task RetryVectorDetailAsync(long id)
    {
        var command = new RetryVectorDetailCommand(id);
        await EventBus.PublishAsync(command);
    }
}