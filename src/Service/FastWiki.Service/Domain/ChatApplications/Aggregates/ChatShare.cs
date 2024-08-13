using FastWiki.Infrastructure.Common.Helper;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

/// <summary>
///     对话分享
/// </summary>
public sealed class ChatShare : Entity<string>, IAuditAggregateRoot<Guid>
{
    protected ChatShare()
    {
    }

    public ChatShare(string name, string chatApplicationId, DateTime expires, long availableToken,
        int availableQuantity)
    {
        Name = name;
        Id = Guid.NewGuid().ToString("N");
        ChatApplicationId = chatApplicationId;
        Expires = expires;
        AvailableToken = availableToken;
        AvailableQuantity = availableQuantity;
        APIKey = "sk-" + StringHelper.GenerateRandomString(18);
    }

    public string Name { get; set; }

    /// <summary>
    ///     绑定应用
    /// </summary>
    public string ChatApplicationId { get; set; }

    /// <summary>
    ///     过期时间
    /// </summary>
    public DateTime? Expires { get; set; }

    /// <summary>
    ///     已用Token
    /// </summary>
    public long UsedToken { get; set; }

    /// <summary>
    ///     可用Token -1则是无限
    /// </summary>
    public long AvailableToken { get; set; }

    /// <summary>
    ///     可用数量
    /// </summary>
    public int AvailableQuantity { get; set; }

    /// <summary>
    ///     请求令牌
    /// </summary>
    public string APIKey { get; set; }

    public Guid Creator { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid Modifier { get; set; }
    public DateTime ModificationTime { get; set; }
}