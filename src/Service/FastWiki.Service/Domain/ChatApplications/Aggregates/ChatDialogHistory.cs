namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

public sealed class ChatDialogHistory : Entity<string>, IFullAggregateRoot<Guid>
{
    /// <summary>
    /// 对话id
    /// </summary>
    public string ChatDialogId { get; set; }

    /// <summary>
    /// 对话内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 对话消耗token
    /// </summary>
    public int TokenConsumption { get; set; }

    /// <summary>
    /// 是否本人
    /// </summary>
    public bool Current { get; set; }

    /// <summary>
    /// 对话类型
    /// </summary>
    public ChatDialogHistoryType Type { get; set; }

    /// <summary>
    /// 引用文件
    /// </summary>
    public List<ReferenceFile> ReferenceFile { get; set; } = new();

    public Guid Creator { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid Modifier { get; set; }
    public DateTime ModificationTime { get; set; }
    public bool IsDeleted { get; set; }

    public void SetId(string id)
    {
        Id = id;
    }

    protected ChatDialogHistory()
    {
    }

    public ChatDialogHistory(string chatDialogId, string content, int tokenConsumption,
        bool current, ChatDialogHistoryType type = ChatDialogHistoryType.Text)
    {
        Id = Guid.NewGuid().ToString("N");
        ChatDialogId = chatDialogId;
        Content = content;
        TokenConsumption = tokenConsumption;
        Current = current;
        Type = type;
    }
}

public class ReferenceFile
{
    public string Name { get; set; }

    public string FilePath { get; set; }

    public string FileId { get; set; }
}