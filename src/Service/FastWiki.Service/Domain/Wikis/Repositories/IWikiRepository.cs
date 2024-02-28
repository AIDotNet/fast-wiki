namespace FastWiki.Service.Domain.Wikis.Repositories;

/// <summary>
/// 知识库仓储
/// </summary>
public interface IWikiRepository : IRepository<Wiki, long>
{
    /// <summary>
    /// 获取知识库列表
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<Wiki>> GetListAsync(string? keyword, int page, int pageSize);

    /// <summary>
    /// 获取知识库数量
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    Task<long> GetCountAsync(string? keyword);

    /// <summary>
    /// 获取知识库详情列表
    /// </summary>
    /// <param name="wikiId"></param>
    /// <param name="keyword"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<WikiDetail>> GetDetailsListAsync(long wikiId, string? keyword, int page, int pageSize);

    /// <summary>
    /// 获取知识库详情数量
    /// </summary>
    /// <param name="wikiId"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    Task<long> GetDetailsCountAsync(long wikiId, string? keyword);

    /// <summary>
    /// 添加知识库详情
    /// </summary>
    /// <param name="wikiDetail"></param>
    /// <returns></returns>
    Task<WikiDetail> AddDetailsAsync(WikiDetail wikiDetail);

    /// <summary>
    /// 删除知识库详情
    /// </summary>
    /// <param name="wikiDetailId"></param>
    /// <returns></returns>
    Task RemoveDetailsAsync(long wikiDetailId);

    /// <summary>
    /// 获取知识库详情信息
    /// </summary>
    /// <param name="wikiDetailId"></param>
    /// <returns></returns>
    Task<WikiDetail> GetDetailsAsync(long wikiDetailId);

    /// <summary>
    /// 批量删除详情
    /// </summary>
    /// <param name="wikiDetailIds"></param>
    /// <returns></returns>
    Task RemoveDetailsAsync(List<long> wikiDetailIds);
}