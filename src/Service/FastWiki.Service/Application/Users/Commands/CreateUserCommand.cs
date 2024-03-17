using FastWiki.Service.Contracts.Users.Dto;

namespace FastWiki.Service.Application.Users.Commands;

/// <summary>
/// 创建用户命令
/// </summary>
/// <param name="Input"></param>
public record CreateUserCommand(CreateUserInput Input) : Command;