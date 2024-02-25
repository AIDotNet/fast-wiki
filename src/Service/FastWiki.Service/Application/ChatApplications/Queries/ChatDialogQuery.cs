namespace FastWiki.Service.Application.ChatApplications.Queries;

public record ChatDialogQuery() : Query<List<ChatDialogDto>>
{
    public override List<ChatDialogDto> Result
    {
        get;
        set;
    }
}