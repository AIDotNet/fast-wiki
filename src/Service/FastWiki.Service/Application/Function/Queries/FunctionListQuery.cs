using FastWiki.Service.Contracts.Function;
using FastWiki.Service.Contracts.Function.Dto;

namespace FastWiki.Service.Application.Function.Queries;

public record FunctionListQuery(int page, int pageSize) : Query<PaginatedListBase<FastWikiFunctionCallDto>>
{
    public override PaginatedListBase<FastWikiFunctionCallDto> Result { get; set; }
}