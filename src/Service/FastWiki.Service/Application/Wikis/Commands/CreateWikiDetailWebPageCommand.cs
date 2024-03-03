namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
/// 创建知识库详情 Web 页面
/// </summary>
/// <param name="Input"></param>
public record CreateWikiDetailWebPageCommand(CreateWikiDetailWebPageInput Input) : Command;