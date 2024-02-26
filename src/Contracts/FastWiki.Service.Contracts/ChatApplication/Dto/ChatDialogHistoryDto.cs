namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public sealed class ChatDialogHistoryDto
{
    public string Id { get; set; }

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

    public DateTime CreationTime { get; set; }
}