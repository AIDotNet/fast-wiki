namespace FastWiki.Service.Contracts.Wikis;

public interface IWikiService
{
    Task CreateAsync(CreateWikiInput input);
    
    Task<WikiDto> GetAsync(long id);

    Task<PaginatedListBase<WikiDto>> GetWikiListAsync(string keyword, int page, int pageSize);
    
    Task RemoveAsync(long id);
    
    Task<PaginatedListBase<WikiDetailDto>> GetWikiDetailsAsync(long wikiId, string? keyword, int page, int pageSize);
}