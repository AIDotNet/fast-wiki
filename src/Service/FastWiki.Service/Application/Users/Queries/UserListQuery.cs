using FastWiki.Service.Contracts.Users.Dto;

namespace FastWiki.Service.Application.Users.Queries;

public record UserListQuery(string? Keyword, int Page, int PageSize) : Query<PaginatedListBase<UserDto>>
{
    public override PaginatedListBase<UserDto> Result { get; set; }
}