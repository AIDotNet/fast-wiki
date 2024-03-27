namespace FastWiki.Service.Application.ChatApplications.Queries;

public record GetHistoryInfoQuery(string historyId) : Query<ChatDialogHistoryDto>
{
    public override ChatDialogHistoryDto Result { get; set; }
}