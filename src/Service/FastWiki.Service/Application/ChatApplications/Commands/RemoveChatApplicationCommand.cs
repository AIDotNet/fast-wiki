namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 删除指定的聊天应用程序命令
/// </summary>
/// <param name="Id"></param>
public record RemoveChatApplicationCommand(string Id) : Command;