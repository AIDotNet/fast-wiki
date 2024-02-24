using FastWiki.Service.Contracts.ChatApplication.Dto;

namespace FastWiki.Service.Application.ChatApplications.Queries;

public record ChatApplicationQuery(int Page, int PageSize) : Query<PaginatedListBase<ChatApplicationDto>>
{
    public override PaginatedListBase<ChatApplicationDto> Result { get; set; }
}