namespace FastWiki.Service.Application.Model.Commands;

/// <summary>
/// 删除指定模型
/// </summary>
/// <param name="Id"></param>
public record RemoveFastModelCommand(string Id): Command;