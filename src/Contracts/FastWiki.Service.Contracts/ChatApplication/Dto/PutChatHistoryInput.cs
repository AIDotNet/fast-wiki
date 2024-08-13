namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public sealed class PutChatHistoryInput
{
    public string Id { get; set; }

    public string Content { get; set; }

    public string? ChatShareId { get; set; }
}