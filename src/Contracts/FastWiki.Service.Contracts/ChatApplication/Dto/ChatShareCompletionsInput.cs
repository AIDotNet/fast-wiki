namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public class ChatShareCompletionsInput
{
    /// <summary>
    ///     对话Id
    /// </summary>
    public string ChatDialogId { get; set; }

    /// <summary>
    ///     分享Id
    /// </summary>
    public string ChatShareId { get; set; }

    /// <summary>
    ///     对话内容
    /// </summary>
    public string Content { get; set; }
}