namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
/// 创建知识库详情
/// </summary>
/// <param name="Input"></param>
public record CreateWikiDetailsCommand(CreateWikiDetailsInput Input) : Command;