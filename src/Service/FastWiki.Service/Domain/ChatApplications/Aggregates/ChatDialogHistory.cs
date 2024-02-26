using FastWiki.Service.Contracts;

namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

public class ChatDialogHistory : Entity<string>, IFullAggregateRoot<Guid>
{
    /// <summary>
    /// 对话id
    /// </summary>
    public string ChatDialogId { get; set; }

    /// <summary>
    /// 应用Id
    /// </summary>
    public string ChatApplicationId { get; set; }

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

    public Guid Creator { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid Modifier { get; set; }
    public DateTime ModificationTime { get; set; }
    public bool IsDeleted { get; set; }

    protected ChatDialogHistory()
    {
    }

    public ChatDialogHistory(string chatApplicationId, string chatDialogId, string content, int tokenConsumption,
        bool current, ChatDialogHistoryType type = ChatDialogHistoryType.Text)
    {
        Id = Guid.NewGuid().ToString("N");
        ChatApplicationId = chatApplicationId;
        ChatDialogId = chatDialogId;
        Content = content;
        TokenConsumption = tokenConsumption;
        Current = current;
        Type = type;
    }
}