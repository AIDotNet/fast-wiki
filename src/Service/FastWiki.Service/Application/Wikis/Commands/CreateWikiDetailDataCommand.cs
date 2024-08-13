namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
///     创建知识库详情指定数据
/// </summary>
/// <param name="Input"></param>
public record CreateWikiDetailDataCommand(CreateWikiDetailDataInput Input) : Command;