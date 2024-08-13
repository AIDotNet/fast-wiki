namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
///     扣款指定分享对话的token
/// </summary>
/// <param name="Id"></param>
/// <param name="Token"></param>
public record DeductTokenCommand(string Id, int Token) : Command;