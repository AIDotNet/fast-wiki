namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
/// 删除指定知识库详情向量数据命令
/// </summary>
public record RemoveWikiDetailVectorQuantityCommand(string DocumentId) : Command;