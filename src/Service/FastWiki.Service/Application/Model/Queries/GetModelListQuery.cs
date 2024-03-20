using FastWiki.Service.Contracts.Model;

namespace FastWiki.Service.Application.Model.Queries;

public record GetModelListQuery(string Keyword, int Page, int PageSize) : Query<PaginatedListBase<FastModelDto>>
{
    public override PaginatedListBase<FastModelDto> Result { get; set; }
}