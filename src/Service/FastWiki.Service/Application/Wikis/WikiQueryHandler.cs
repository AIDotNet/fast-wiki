using FastWiki.Service.Application.Wikis.Queries;

namespace FastWiki.Service.Application.Wikis;

public sealed class WikiQueryHandler(IWikiRepository wikiRepository)
{
    [EventHandler]
    public async Task GetWiki(WikiQuery query)
    {
        var wiki = await wikiRepository.FindAsync(query.Id);

        if (wiki == null)
        {
            throw new UserFriendlyException("知识库不存在");
        }

        query.Result = wiki.Map<WikiDto>();
    }

    [EventHandler]
    public async Task GetWikiList(WikiListQuery query)
    {
        var wikis = await wikiRepository.GetListAsync(query.Keyword, query.Page, query.PageSize);

        var count = await wikiRepository.GetCountAsync(query.Keyword);

        query.Result = new PaginatedListBase<WikiDto>()
        {
            Result = wikis.Map<List<WikiDto>>(),
            Total = count
        };
    }
    
    [EventHandler]
    public async Task GetWikiDetails(WikiDetailsQuery query)
    {
        var wikis = await wikiRepository.GetDetailsListAsync(query.WikiId, query.Keyword, query.Page, query.PageSize);

        var count = await wikiRepository.GetDetailsCountAsync(query.WikiId, query.Keyword);

        query.Result = new PaginatedListBase<WikiDetailDto>()
        {
            Result = wikis.Map<List<WikiDetailDto>>(),
            Total = count
        };
        
    }
}