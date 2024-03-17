namespace FastWiki.Service.Application.ChatApplications.Queries;

public record ChatApplicationQuery(int Page, int PageSize, Guid userId) : Query<PaginatedListBase<ChatApplicationDto>>
{
    public override PaginatedListBase<ChatApplicationDto> Result { get; set; }
}