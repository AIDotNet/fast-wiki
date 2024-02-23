using FastWiki.Service.Contracts.ChatApplication.Dto;

namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
/// 创建聊天应用程序命令
/// </summary>
/// <param name="Input"></param>
public record CreateChatApplicationCommand(CreateChatApplicationInput Input) : Command;