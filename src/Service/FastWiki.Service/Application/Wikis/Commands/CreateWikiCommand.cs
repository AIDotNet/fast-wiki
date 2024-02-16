namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
/// 添加知识库命令
/// </summary>
/// <param name="Input"></param>
public record CreateWikiCommand(CreateWikiInput Input) : Command;