namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

public sealed class ChatDialog : Entity<string>, IFullAggregateRoot<Guid>
{
    public string Name { get; set; }

    /// <summary>
    /// 如果Type是ChatApplication则来源是应用
    /// 如果Type是ChatShare则来源分享对话的游客
    /// </summary>
    public string ChatId { get; set; }

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

    public ChatDialog(string name, string chatId, string description)
    {
        Id = Guid.NewGuid().ToString("N");
        Name = name;
        ChatId = chatId;
        Description = description;
        Type = ChatDialogType.ChatApplication;
    }

    public void SetType(ChatDialogType type)
    {
        Type = type;
    }
}