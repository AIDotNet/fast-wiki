namespace FastWiki.Service.Application.Wikis.Queries;

/// <summary>
/// 获取知识库查询
/// </summary>
/// <param name="Keyword"></param>
/// <param name="Page"></param>
/// <param name="PageSize"></param>
public record WikiListQuery(Guid userId, string? Keyword, int Page, int PageSize) : Query<PaginatedListBase<WikiDto>>()
{
    public override PaginatedListBase<WikiDto> Result { get; set; }
}
