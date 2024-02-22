namespace FastWiki.Service.Application.Wikis;

public sealed class WikiQueryHandler(IWikiRepository wikiRepository, MemoryServerless memoryServerless)
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

    [EventHandler]
    public async Task WikiDetailVectorQuantityAsync(WikiDetailVectorQuantityQuery query)
    {

        var memoryDbs = memoryServerless.Orchestrator.GetMemoryDbs();

        var result = new PaginatedListBase<WikiDetailVectorQuantityDto>();

        var dto = new List<WikiDetailVectorQuantityDto>();

        var entity = await wikiRepository.GetDetailsAsync(long.Parse(query.WikiDetailId));

        result.Total = entity.DataCount;

        foreach (var memoryDb in memoryDbs)
        {
            // 通过pageSize和page获取到最大数量
            var limit = query.PageSize * query.Page;
            if (limit < 10)
            {
                limit = 10;
            }

            var filter = new MemoryFilter();
            filter.Add("wikiDetailId", query.WikiDetailId);

            int size = 0;
            await foreach (var item in memoryDb.GetListAsync("wiki", new List<MemoryFilter>()
                           {
                               filter
                           }, limit, true))
            {
                size++;
                if (size > query.PageSize * query.Page)
                {
                    continue;
                }

                dto.Add(new WikiDetailVectorQuantityDto()
                {
                    Content = item.Payload["text"].ToString() ?? string.Empty,
                    FileId = item.Tags["fileId"].FirstOrDefault() ?? string.Empty,
                    Id = item.Id,
                    WikiDetailId = item.Tags["wikiDetailId"].FirstOrDefault() ?? string.Empty,
                    Document_Id = item.Tags["__document_id"].FirstOrDefault() ?? string.Empty
                });
            }
        }
        
        result.Result = dto;

        query.Result = result;
    }
}