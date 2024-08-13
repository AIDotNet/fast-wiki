namespace FastWiki.Service.Application.ChatApplications.Commands;

/// <summary>
///     删除提问
/// </summary>
/// <param name="Id"></param>
public record RemoveQuestionsCommand(string Id) : Command;