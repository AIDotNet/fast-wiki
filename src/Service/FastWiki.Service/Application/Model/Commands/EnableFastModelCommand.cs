namespace FastWiki.Service.Application.Model.Commands;

/// <summary>
/// 启用|禁用模型
/// </summary>
/// <param name="Id"></param>
/// <param name="Enable"></param>
public record EnableFastModelCommand(string Id, bool Enable) : Command;