namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
///     编辑聊天应用程序命令
/// </summary>
/// <param name="Input"></param>
public record UpdateChatApplicationCommand(UpdateChatApplicationInput Input) : Command;