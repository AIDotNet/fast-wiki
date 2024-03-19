namespace FastWiki.Service.Application.Wikis.Commands;

/// <summary>
/// 重试量化
/// </summary>
/// <param name="Id"></param>
public record RetryVectorDetailCommand(long Id):Command;