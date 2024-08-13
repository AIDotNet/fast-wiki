namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public class CreateChatDialogInput
{
    public string Name { get; set; }

    public string ChatId { get; set; }

    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///     应用Id
    /// </summary>
    public string ApplicationId { get; set; }

    public ChatDialogType Type { get; set; }
}