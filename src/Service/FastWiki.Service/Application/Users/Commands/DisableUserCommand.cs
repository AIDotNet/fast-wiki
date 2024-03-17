namespace FastWiki.Service.Application.Users.Commands;

/// <summary>
/// ½ûÓÃÓÃ»§ÃüÁî
/// </summary>
/// <param name="Id"></param>
/// <param name="IsDisable"></param>
public record DisableUserCommand(Guid Id,bool IsDisable):Command;