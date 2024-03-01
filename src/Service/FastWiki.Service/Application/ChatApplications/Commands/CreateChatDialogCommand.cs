namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 创建对话
/// </summary>
/// <param name="Input"></param>
public record CreateChatDialogCommand(CreateChatDialogInput Input):Command;

/// <summary>
/// 创建分享对话
/// </summary>
/// <param name="Input"></param>
public record CreateChatDialogChatShareCommand(CreateChatDialogInput Input):Command;