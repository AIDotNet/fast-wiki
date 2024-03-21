namespace FastWiki.Service.Application.Model.Commands;

/// <summary>
/// ¼ÆËãÄ£ÐÍ
/// </summary>
public record FastModelComputeTokenCommand(string Id,int RequestToken,int CompleteToken) : Command;