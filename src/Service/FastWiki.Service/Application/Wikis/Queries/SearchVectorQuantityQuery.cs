namespace FastWiki.Service.Application.Wikis.Queries;

/// <summary>
///     向量搜索查询
/// </summary>
/// <param name="WikiId"></param>
/// <param name="Search"></param>
/// <param name="MinRelevance"></param>
public record SearchVectorQuantityQuery(long WikiId, string Search, double MinRelevance = 0.0)
    : Query<SearchVectorQuantityResult>
{
    public override SearchVectorQuantityResult Result { get; set; }
}