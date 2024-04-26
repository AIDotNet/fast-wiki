namespace FastWiki.Service.Application.Function.Commands;

/// <summary>
/// 删除function call
/// </summary>
/// <param name="Id"></param>
public record RemoveFunctionCommand(long Id) : Command;