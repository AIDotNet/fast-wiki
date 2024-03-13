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
    /// 编辑知识库基本信息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task UpdateAsync(WikiDto dto);

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
    /// 创建知识库详情 Web 页面
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateWikiDetailWebPageInputAsync(CreateWikiDetailWebPageInput input);

    /// <summary>
    /// 创建知识库详情数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateWikiDetailDataAsync(CreateWikiDetailDataInput input);

    /// <summary>
    /// 获取知识库详情列表
    /// </summary>
    /// <param name="wikiId"></param>
    /// <param name="keyword"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PaginatedListBase<WikiDetailDto>> GetWikiDetailsAsync(long wikiId,WikiQuantizationState? state, string? keyword, int page, int pageSize);

    /// <summary>
    /// 删除知识库详情列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveDetailsAsync(long id);

    /// <summary>
    /// 获取指定知识库详情的向量数据
    /// </summary>
    /// <param name="wikiDetailId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PaginatedListBase<WikiDetailVectorQuantityDto>> GetWikiDetailVectorQuantityAsync(string wikiDetailId, int page, int pageSize);

    /// <summary>
    /// 删除指定知识库详情的向量数据
    /// </summary>
    /// <param name="documentId"></param>
    /// <returns></returns>
    Task RemoveDetailVectorQuantityAsync(string documentId);

    /// <summary>
    /// 获取搜索指定知识库的向量数据
    /// </summary>
    /// <param name="wikiId"></param>
    /// <param name="search"></param>
    /// <param name="minRelevance"></param>
    /// <returns></returns>
    Task<SearchVectorQuantityResult> GetSearchVectorQuantityAsync(long wikiId, string search,
        double minRelevance = 0.6);

    /// <summary>
    /// 删除指定知识库详情的向量数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveDetailsVectorAsync(string id);
}