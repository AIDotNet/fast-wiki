namespace FastWiki.Service.Application.Wikis.Queries;

/// <summary>
/// 获取知识库详情向量数据
/// </summary>
/// <param name="WikiDetailId"></param>
/// <param name="Page"></param>
/// <param name="PageSize"></param>
public record WikiDetailVectorQuantityQuery(string WikiDetailId, int Page, int PageSize) : Query<PaginatedListBase<WikiDetailVectorQuantityDto>>
{
    public override PaginatedListBase<WikiDetailVectorQuantityDto> Result
    {
        get;
        set;
    }
}