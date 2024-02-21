namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
/// 删除知识库详情命令
/// </summary>
/// <param name="Id"></param>
public record RemoveWikiDetailsCommand(long Id):Command;