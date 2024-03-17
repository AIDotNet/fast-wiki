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

/// <summary>
/// 获取知识库详情查询
/// </summary>
/// <param name="Id"></param>
public record WikiQuery(long Id) : Query<WikiDto>()
{
    public override WikiDto Result { get; set; }
}