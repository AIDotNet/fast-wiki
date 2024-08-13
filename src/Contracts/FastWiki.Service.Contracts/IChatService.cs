using FastWiki.Service.Contracts.ChatApplication.Dto;

namespace FastWiki.Service.Contracts;

public interface IChatService
{
    /// <summary>
    ///     智能对话
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    IAsyncEnumerable<CompletionsDto> CompletionsAsync(CompletionsInput input);
}