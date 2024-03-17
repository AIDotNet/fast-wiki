namespace FastWiki.Service.Application.ChatApplications.Queries;

public record ChatDialogQuery(string chatId, bool all, Guid userId) : Query<List<ChatDialogDto>>
{
    public override List<ChatDialogDto> Result
    {
        get;
        set;
    }
}
public record ChatShareDialogQuery(string chatId) : Query<List<ChatDialogDto>>
{
    public override List<ChatDialogDto> Result
    {
        get;
        set;
    }
}
