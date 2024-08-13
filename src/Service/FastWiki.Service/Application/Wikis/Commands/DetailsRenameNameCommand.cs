namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
///     修改知识库详情名称
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
public record DetailsRenameNameCommand(long Id, string Name) : Command;