namespace FastWiki.Service.Application.ChatApplications.Queries;

public record ChatDialogQuery(string chatId) : Query<List<ChatDialogDto>>
{
    public override List<ChatDialogDto> Result
    {
        get;
        set;
    }
}
