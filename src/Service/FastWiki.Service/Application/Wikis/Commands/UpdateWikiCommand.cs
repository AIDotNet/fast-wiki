namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
/// 编辑知识库
/// </summary>
/// <param name="Dto"></param>
public record UpdateWikiCommand(WikiDto Dto):Command;