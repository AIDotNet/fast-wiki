namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public sealed class CompletionsInput
{
    /// <summary>
    /// 对话Id
    /// </summary>
    public string ChatDialogId { get; set; }

    /// <summary>
    /// 应用Id
    /// </summary>
    public string ChatApplicationId { get; set; }

    public List<ChatMessagesInput> Messages { get; set; }
}

public class ChatMessagesInput
{
    public string Role { get; set; }

    public string Content { get; set; }
}