using System.Diagnostics;
using FastWiki.Service.Service;

namespace FastWiki.Service.Application.Wikis;

public sealed class WikiQueryHandler(
    IWikiRepository wikiRepository,
    WikiMemoryService wikiMemoryService,
    mem0.NET.Services.MemoryService memoryService,
    IMapper mapper,
    IFileStorageRepository fileStorageRepository)
{
    [EventHandler]
    public async Task GetWikiList(WikiListQuery query)
    {
        var wikis = await wikiRepository.GetListAsync(query.userId, query.Keyword, query.Page, query.PageSize);

        var count = await wikiRepository.GetCountAsync(query.userId, query.Keyword);

        query.Result = new PaginatedListBase<WikiDto>
        {
            Result = mapper.Map<List<WikiDto>>(wikis),
            Total = count
        };
    }

    [EventHandler]
    public async Task GetWikiDetails(WikiDetailsQuery query)
    {
        var wikis = await wikiRepository.GetDetailsListAsync(query.WikiId, query.State, query.Keyword, query.Page,
            query.PageSize);

        var count = await wikiRepository.GetDetailsCountAsync(query.WikiId, query.State, query.Keyword);

        query.Result = new PaginatedListBase<WikiDetailDto>
        {
            Result = mapper.Map<List<WikiDetailDto>>(wikis),
            Total = count
        };
    }

    [EventHandler]
    public async Task WikiDetailVectorQuantityAsync(WikiDetailVectorQuantityQuery query)
    {
        var wiki = await wikiRepository.WikiDetailGetWikiAsync(long.Parse(query.WikiDetailId));

        if (wiki == null)
            throw new UserFriendlyException("知识库不存在 with id: " + query.WikiDetailId);

        if (wiki.VectorType == VectorType.Mem0)
        {
            int limit = query.PageSize * (query.Page);

            var result = await memoryService.GetMemoryAll(null, wiki.Id.ToString(), query.WikiDetailId, (uint)limit);

            var dto = new List<WikiDetailVectorQuantityDto>();

            var index = 0;
            foreach (var item in result)
            {
                // 需要跳过前面的数据
                if (index < query.PageSize * (query.Page - 1))
                {
                    index++;
                    continue;
                }

                if (index > query.PageSize * query.Page) break;

                dto.Add(new WikiDetailVectorQuantityDto
                {
                    Content = item.MetaData["metaData"],
                    FileId = item.MetaData.FirstOrDefault(x => x.Key == "fileId").Value ?? string.Empty,
                    Id = item.Id.ToString(),
                    Index = index,
                    WikiDetailId = item.MetaData["wikiDetailId"] ?? string.Empty,
                    Document_Id = item.Id.ToString()
                });
            }
            
            query.Result = new PaginatedListBase<WikiDetailVectorQuantityDto>
            {
                Result = dto,
                Total = result.Count
            };

            return;
        }
        else
        {
            var memoryServerless = wikiMemoryService.CreateMemoryServerless(wiki.EmbeddingModel, wiki.Model);
            var memoryDbs = memoryServerless.Orchestrator.GetMemoryDbs();

            var result = new PaginatedListBase<WikiDetailVectorQuantityDto>();

            var dto = new List<WikiDetailVectorQuantityDto>();

            var entity = await wikiRepository.GetDetailsAsync(long.Parse(query.WikiDetailId));

            result.Total = entity.DataCount;

            foreach (var memoryDb in memoryDbs)
            {
                // 通过pageSize和page获取到最大数量
                var limit = query.PageSize * query.Page;
                if (limit < 10) limit = 10;

                var filter = new MemoryFilter().ByDocument(query.WikiDetailId);

                var size = 0;
                await foreach (var item in memoryDb.GetListAsync("wiki", new List<MemoryFilter>
                               {
                                   filter
                               }, limit, true))
                {
                    size++;
                    if (size < query.PageSize * (query.Page - 1)) continue;

                    if (size > query.PageSize * query.Page) break;

                    dto.Add(new WikiDetailVectorQuantityDto
                    {
                        Content = item.Payload["text"].ToString() ?? string.Empty,
                        FileId = item.Tags.FirstOrDefault(x => x.Key == "fileId").Value?.FirstOrDefault() ??
                                 string.Empty,
                        Id = item.Id,
                        Index = size,
                        WikiDetailId = item.Tags["wikiDetailId"].FirstOrDefault() ?? string.Empty,
                        Document_Id = item.Tags["__document_id"].FirstOrDefault() ?? string.Empty
                    });
                }
            }

            result.Result = dto;

            query.Result = result;
        }
    }

    [EventHandler]
    public async Task SearchVectorQuantityAsync(SearchVectorQuantityQuery query)
    {
        var stopwatch = Stopwatch.StartNew();

        var wiki = await wikiRepository.FindAsync(query.WikiId);

        var memoryServerless = wikiMemoryService.CreateMemoryServerless(wiki.EmbeddingModel, wiki.Model);
        var searchResult = await memoryServerless.SearchAsync(query.Search, "wiki",
            new MemoryFilter().ByTag("wikiId", query.WikiId.ToString()), minRelevance: query.MinRelevance, limit: 5);

        stopwatch.Stop();

        var searchVectorQuantityResult = new SearchVectorQuantityResult();

        searchVectorQuantityResult.ElapsedTime = stopwatch.ElapsedMilliseconds;

        searchVectorQuantityResult.Result = new List<SearchVectorQuantityDto>();

        foreach (var resultResult in searchResult.Results)
            searchVectorQuantityResult.Result.AddRange(resultResult.Partitions.Select(partition =>
                new SearchVectorQuantityDto
                {
                    Content = partition.Text,
                    DocumentId = resultResult.DocumentId,
                    Relevance = partition.Relevance,
                    FileId = partition.Tags["fileId"].FirstOrDefault() ?? string.Empty
                }));

        var fileIds = new List<long>();
        fileIds.AddRange(searchVectorQuantityResult.Result.Select(x =>
        {
            if (long.TryParse(x.FileId, out var i)) return i;

            return -1;
        }).Where(x => x > 0));

        var files = await fileStorageRepository.GetListAsync(fileIds.ToArray());

        foreach (var quantityDto in searchVectorQuantityResult.Result)
        {
            var file = files.FirstOrDefault(x => x.Id.ToString() == quantityDto.FileId);
            quantityDto.FullPath = file?.Path;

            quantityDto.FileName = file?.Name;
        }

        query.Result = searchVectorQuantityResult;
    }
}