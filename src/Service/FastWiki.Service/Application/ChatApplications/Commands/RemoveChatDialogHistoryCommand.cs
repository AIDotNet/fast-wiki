namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 删除指定记录命令
/// </summary>
/// <param name="Id"></param>
public record RemoveChatDialogHistoryCommand(string Id):Command;