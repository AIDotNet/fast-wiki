namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public sealed class CompletionsInput
{
    /// <summary>
    ///     对话Id
    /// </summary>
    public string ChatDialogId { get; set; }

    /// <summary>
    ///     应用Id
    /// </summary>
    public string ChatId { get; set; }

    public string Content { get; set; }
}