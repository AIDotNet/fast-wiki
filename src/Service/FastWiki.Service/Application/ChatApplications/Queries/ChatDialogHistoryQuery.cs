namespace FastWiki.Service.Application.ChatApplications.Queries;

/// <summary>
/// 获取对话记录查询
/// </summary>
/// <param name="ChatDialogId"></param>
/// <param name="Page"></param>
/// <param name="PageSize"></param>
public record ChatDialogHistoryQuery(string ChatDialogId, int Page, int PageSize)
    : Query<PaginatedListBase<ChatDialogHistoryDto>>
{
    public override PaginatedListBase<ChatDialogHistoryDto> Result { get; set; }
}