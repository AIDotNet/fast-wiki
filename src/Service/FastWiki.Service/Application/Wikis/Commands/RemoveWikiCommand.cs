namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
/// 移除知识库命令
/// </summary>
/// <param name="Id"></param>
public record RemoveWikiCommand(long Id) : Command;