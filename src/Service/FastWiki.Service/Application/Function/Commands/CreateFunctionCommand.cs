using FastWiki.Service.Contracts.Function;
using FastWiki.Service.Contracts.Function.Dto;

namespace FastWiki.Service.Application.Function.Commands;

/// <summary>
/// 创建function call
/// </summary>
/// <param name="FunctionCallInput"></param>
public record CreateFunctionCommand(FastWikiFunctionCallInput FunctionCallInput) : Command;