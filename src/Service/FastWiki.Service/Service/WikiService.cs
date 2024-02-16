namespace FastWiki.Service.Service;

/// <summary>
/// 知识库服务
/// </summary>
public sealed class WikiService : ApplicationService<WikiService>, IWikiService
{
    /// <summary>
    /// 创建知识库
    /// </summary>
    /// <param name="input"></param>
    public async Task CreateAsync(CreateWikiInput input)
    {
        var command = new CreateWikiCommand(input);

        await EventBus.PublishAsync(command);
    }

    /// <summary>
    /// 获取知识库
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<WikiDto> GetAsync(long id)
    {
        var query = new WikiQuery(id);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    /// <summary>
    /// 获取知识库列表
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<PaginatedListBase<WikiDto>> GetWikiListAsync(string? keyword, int page, int pageSize)
    {
        var query = new WikiListQuery(keyword, page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }
    
    /// <summary>
    /// 删除知识库
    /// </summary>
    /// <param name="id"></param>
    public async Task RemoveAsync(long id)
    {
        var command = new RemoveWikiCommand(id);

        await EventBus.PublishAsync(command);
    }

    /// <summary>
    /// 获取知识库详情列表
    /// </summary>
    /// <param name="wikiId"></param>
    /// <param name="keyword"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<PaginatedListBase<WikiDetailDto>> GetWikiDetailsAsync(long wikiId, string? keyword, int page, int pageSize)
    {
        var query = new WikiDetailsQuery(wikiId, keyword, page, pageSize);

        await EventBus.PublishAsync(query);
        
        return query.Result;
    }
}