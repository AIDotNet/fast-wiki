using FastWiki.Service.Domain.Function.Aggregates;

namespace FastWiki.Service.Application.Function.Queries;

public record ChatApplicationFunctionCallQuery(params long[] Ids): Query<IEnumerable<FastWikiFunctionCall>>
{
    public override IEnumerable<FastWikiFunctionCall> Result { get; set; }
}