namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 创建对话记录
/// </summary>
/// <param name="Input"></param>
public record CreateChatDialogHistoryCommand(CreateChatDialogHistoryInput Input): Command;