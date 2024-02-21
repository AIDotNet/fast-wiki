namespace FastWiki.Service.Contracts.Wikis;

public interface IWikiService
{
    /// <summary>
    /// 创建知识库
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateAsync(CreateWikiInput input);

    /// <summary>
    /// 获取知识库详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<WikiDto> GetAsync(long id);

    /// <summary>
    /// 获取知识库列表
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PaginatedListBase<WikiDto>> GetWikiListAsync(string keyword, int page, int pageSize);

    /// <summary>
    /// 删除知识库
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(long id);

    /// <summary>
    /// 创建知识库详情
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateWikiDetailsAsync(CreateWikiDetailsInput input);

    /// <summary>
    /// 获取知识库详情列表
    /// </summary>
    /// <param name="wikiId"></param>
    /// <param name="keyword"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PaginatedListBase<WikiDetailDto>> GetWikiDetailsAsync(long wikiId, string? keyword, int page, int pageSize);

    /// <summary>
    /// 删除知识库详情列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveDetailsAsync(long id);
}