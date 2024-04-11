namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 删除指定记录命令
/// </summary>
/// <param name="Id"></param>
public record RemoveChatDialogHistoryCommand(string Id):Command;

/// <summary>
/// 删除指定对话所有记录命令
/// </summary>
/// <param name="dialogId"></param>
public record RemovesChatDialogHistoryCommand(string dialogId):Command;