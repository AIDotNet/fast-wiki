using FastWiki.Service.Contracts.Function.Dto;

namespace FastWiki.Service.Application.Function.Queries;

public record GetFunctionCallSelectQuery():Query<List<FastWikiFunctionCallSelectDto>>
{
    public override List<FastWikiFunctionCallSelectDto> Result { get; set; }
}