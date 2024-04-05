using FastWiki.Service.Contracts.Function;
using FastWiki.Service.Contracts.Function.Dto;

namespace FastWiki.Service.Application.Function.Commands;

/// <summary>
/// 编辑function call
/// </summary>
/// <param name="Dto"></param>
public record UpdateFunctionCommand(FastWikiFunctionCallDto Dto): Command;