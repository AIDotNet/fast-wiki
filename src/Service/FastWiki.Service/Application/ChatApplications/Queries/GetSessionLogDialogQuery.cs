namespace FastWiki.Service.Application.ChatApplications.Queries;

public record GetSessionLogDialogQuery(string chatApplicationId, int page, int pageSize, Guid userId) : Query<PaginatedListBase<ChatDialogDto>>
{
    public override PaginatedListBase<ChatDialogDto> Result
    {
        get;
        set;
    }
}