namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public class ChatShareDto
{
    public string Id { get; set; }
    public string Name { get; set; }

    /// <summary>
    /// 绑定应用
    /// </summary>
    public string ChatApplicationId { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime? Expires { get; set; }

    /// <summary>
    /// 已用Token
    /// </summary>
    public long UsedToken { get; set; }
    
    /// <summary>
    /// 可用Token -1则是无限
    /// </summary>
    public long AvailableToken { get; set; }

    /// <summary>
    /// 可用数量
    /// </summary>
    public int AvailableQuantity { get; set; }

    /// <summary>
    /// 请求令牌
    /// </summary>
    public string APIKey { get; set; }
}