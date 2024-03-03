namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

public sealed class ChatDialog : Entity<string>, IFullAggregateRoot<Guid>
{
    public string Name { get; set; }

    /// <summary>
    /// 游客Id如果是游客对话
    /// </summary>
    public string? ChatId { get; set; }

    /// <summary>
    /// 应用Id
    /// </summary>
    public string ApplicationId { get; set; }
    
    public string Description { get; set; }

    public ChatDialogType Type { get; set; }

    public Guid Creator { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid Modifier { get; set; }

    public DateTime ModificationTime { get; set; }

    public bool IsDeleted { get; set; }

    protected ChatDialog()
    {
    }

    public ChatDialog(string name, string chatId, string description,string applicationId)
    {
        Id = Guid.NewGuid().ToString("N");
        Name = name;
        ChatId = chatId;
        Description = description;
        Type = ChatDialogType.ChatApplication;
        ApplicationId = applicationId;
    }

    public void SetType(ChatDialogType type)
    {
        Type = type;
    }
}