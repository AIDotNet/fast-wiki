namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 编辑对话
/// </summary>
/// <param name="Input"></param>
public record UpdateChatDialogCommand(ChatDialogDto Input):Command;