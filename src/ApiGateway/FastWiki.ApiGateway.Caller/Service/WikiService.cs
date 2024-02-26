using FastWiki.ApiGateway.caller.Service;
using FastWiki.Service.Contracts.Wikis.Dto;
using Masa.Utils.Models;

namespace FastWiki.ApiGateway.Caller.Service;

public sealed class WikiService(ICaller caller, IHttpClientFactory httpClientFactory) : ServiceBase(caller,httpClientFactory), IWikiService
{
    protected override string BaseUrl { get; set; } = "Wikis";

    public async Task CreateAsync(CreateWikiInput input)
    {
        await PostAsync(nameof(CreateAsync), input);
    }

    public async Task<WikiDto> GetAsync(long id)
    {
        return await GetAsync<WikiDto>(nameof(GetAsync) + "/" + id);
    }

    public async Task<PaginatedListBase<WikiDto>> GetWikiListAsync(string keyword, int page, int pageSize)
    {
        return await GetAsync<PaginatedListBase<WikiDto>>(nameof(GetWikiListAsync), new Dictionary<string, string>()
        {
            { "keyword", keyword },
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        });
    }

    public async Task RemoveAsync(long id)
    {
        await DeleteAsync(nameof(RemoveAsync) + "/" + id);
    }

    public async Task CreateWikiDetailsAsync(CreateWikiDetailsInput input)
    {
        await PostAsync(nameof(CreateWikiDetailsAsync), input);
    }

    public async Task<PaginatedListBase<WikiDetailDto>> GetWikiDetailsAsync(long wikiId, string? keyword, int page,
        int pageSize)
    {
        return await GetAsync<PaginatedListBase<WikiDetailDto>>(nameof(GetWikiDetailsAsync),
            new Dictionary<string, string>()
            {
                { "wikiId", wikiId.ToString() },
                { "keyword", keyword },
                { "page", page.ToString() },
                { "pageSize", pageSize.ToString() }
            });
    }

    public async Task RemoveDetailsAsync(long id)
    {
        await DeleteAsync(nameof(RemoveDetailsAsync) + "/" + id);
    }

    public async Task<PaginatedListBase<WikiDetailVectorQuantityDto>> GetWikiDetailVectorQuantityAsync(
        string wikiDetailId, int page, int pageSize)
    {
        return await GetAsync<PaginatedListBase<WikiDetailVectorQuantityDto>>(nameof(GetWikiDetailVectorQuantityAsync),
            new Dictionary<string, string>()
            {
                { "wikiDetailId", wikiDetailId },
                { "page", page.ToString() },
                { "pageSize", pageSize.ToString() }
            });
    }

    public async Task RemoveDetailVectorQuantityAsync(string documentId)
    {
        await base.DeleteAsync(nameof(RemoveDetailVectorQuantityAsync) + "?documentId=" + documentId);
    }

    public async Task<SearchVectorQuantityResult> GetSearchVectorQuantityAsync(long wikiId, string search,
        double minRelevance = 0D)
    {
        return await GetAsync<SearchVectorQuantityResult>(nameof(GetSearchVectorQuantityAsync),
            new Dictionary<string, string>()
            {
                { "wikiId", wikiId.ToString() },
                { "search", search },
                { "minRelevance",minRelevance.ToString() }
            });
    }
}