using System.Web;
using FastWiki.Service.Backgrounds;

namespace FastWiki.Service.Service;

/// <summary>
///     知识库服务
/// </summary>
public sealed class WikiService(IWikiRepository wikiRepository) : ApplicationService<WikiService>
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
        var wiki = await wikiRepository.FindAsync(id);

        if (wiki == null) throw new UserFriendlyException("知识库不存在");

        return Mapper.Map<WikiDto>(wiki);
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
        var wikis = await wikiRepository.GetListAsync(UserContext.GetUserId<Guid>(), keyword, page, pageSize);

        var count = await wikiRepository.GetCountAsync(UserContext.GetUserId<Guid>(), keyword);

        return new PaginatedListBase<WikiDto>
        {
            Result = Mapper.Map<List<WikiDto>>(wikis),
            Total = count
        };
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

    [Authorize]
    public async Task CreateWikiDetailWebPageInputAsync(CreateWikiDetailWebPageInput input)
    {
        var command = new CreateWikiDetailWebPageCommand(input);

        await EventBus.PublishAsync(command);
    }

    [Authorize]
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

    [Authorize]
    public async Task RemoveDetailsVectorAsync(string id)
    {
        var command = new RemoveDetailsVectorCommand(HttpUtility.UrlDecode(id));

        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task RetryVectorDetailAsync(long id)
    {
        var command = new RetryVectorDetailCommand(id);
        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public Task DetailsRenameNameAsync(long id, string name)
    {
        var command = new DetailsRenameNameCommand(id, name);
        return EventBus.PublishAsync(command);
    }

    /// <summary>
    ///     量化状态检查
    /// </summary>
    /// <param name="wikiId"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<List<CheckQuantizationStateDto>> CheckQuantizationStateAsync(long wikiId)
    {
        var values = QuantizeBackgroundService.CacheWikiDetails.Values.Where(x => x.Item1.WikiId == wikiId).ToList();

        if (values.Any())
            return values.Select(x => new CheckQuantizationStateDto
            {
                WikiId = x.Item1.WikiId,
                FileName = x.Item1.FileName,
                State = x.Item1.State
            }).ToList();

        return [];
    }
}