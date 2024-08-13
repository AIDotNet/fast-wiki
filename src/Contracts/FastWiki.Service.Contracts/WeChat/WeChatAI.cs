namespace FastWiki.Service.Contracts.WeChat;

public class WeChatAI
{
    /// <summary>
    ///     对话内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     应用Id
    /// </summary>
    public string SharedId { get; set; }

    /// <summary>
    ///     内容Id
    /// </summary>
    public string MessageId { get; set; }
}