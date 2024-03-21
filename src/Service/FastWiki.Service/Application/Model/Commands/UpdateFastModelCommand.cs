using FastWiki.Service.Contracts.Model;

namespace FastWiki.Service.Application.Model.Commands;

/// <summary>
/// ±à¼­Ä£ÐÍ
/// </summary>
/// <param name="Dto"></param>
public record UpdateFastModelCommand(FastModelDto Dto):Command;