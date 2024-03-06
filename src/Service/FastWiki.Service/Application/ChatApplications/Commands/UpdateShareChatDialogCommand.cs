namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 编辑分享对话
/// </summary>
/// <param name="Input"></param>
public record UpdateShareChatDialogCommand(ChatDialogDto Input):Command;