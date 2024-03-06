namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 删除分享对话
/// </summary>
/// <param name="ChatId"></param>
/// <param name="Id"></param>
public record RemoveShareDialogCommand(string ChatId,string Id):Command;