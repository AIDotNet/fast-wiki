namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 删除对话
/// </summary>
/// <param name="Id"></param>
public record RemoveChatDialogCommand(string Id) : Command;