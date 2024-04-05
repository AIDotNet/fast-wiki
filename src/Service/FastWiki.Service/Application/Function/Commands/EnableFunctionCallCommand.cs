namespace FastWiki.Service.Application.Function.Commands;

/// <summary>
/// 启用/禁用function call
/// </summary>
/// <param name="Id"></param>
/// <param name="Enable"></param>
public record EnableFunctionCallCommand(long Id,bool Enable):Command;