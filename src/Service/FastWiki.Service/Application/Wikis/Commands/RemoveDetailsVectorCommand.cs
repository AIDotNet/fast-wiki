namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
/// 删除知识库详情的指定向量数据
/// </summary>
/// <param name="Id"></param>
public record RemoveDetailsVectorCommand(string Id):Command;