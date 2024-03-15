namespace FastWiki.Service.Application.Users.Commands;

/// <summary>
/// É¾³ıÓÃ»§ÃüÁî
/// </summary>
/// <param name="Id"></param>
public record DeleteUserCommand(Guid Id):Command;