namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 创建对话记录
/// </summary>
/// <param name="ApplicationId"></param>
/// <param name="Question"></param>
public record CreateChatRecordCommand(string ApplicationId, string Question) : Command;