using FastWiki.Service.Contracts.Model;

namespace FastWiki.Service.Application.Model.Commands;

/// <summary>
/// 创建模型
/// </summary>
/// <param name="Input"></param>
public record CreateFastModeCommand(CreateFastModeInput Input) : Command;