namespace FastWiki.Service.Application.Wikis.Queries;

/// <summary>
/// 获取知识库详情列表
/// </summary>
/// <param name="WikiId"></param>
/// <param name="Keyword"></param>
/// <param name="Page"></param>
/// <param name="PageSize"></param>
public record WikiDetailsQuery(long WikiId,string? Keyword, int Page, int PageSize) : Query<PaginatedListBase<WikiDetailDto>>()
{
    public override PaginatedListBase<WikiDetailDto> Result { get; set; }
}