namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 删除分享对话
/// </summary>
/// <param name="Id"></param>
public record RemoveChatShareCommand(string Id) : Command;