using FastWiki.Infrastructure.Common.Helper;
using Masa.BuildingBlocks.Ddd.Domain.Entities;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace FastWiki.Service.Entities;

public sealed class ChatApplicationShare : Entity<string>, IAuditAggregateRoot<Guid>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 绑定应用
    /// </summary>
    public string ChatApplicationId { get; private set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime? Expires { get; private set; }

    /// <summary>
    /// 已用Token
    /// </summary>
    public long UsedToken { get; private set; }

    /// <summary>
    /// 可用Token -1则是无限
    /// </summary>
    public long AvailableToken { get; private set; }

    /// <summary>
    /// 可用数量
    /// </summary>
    public int AvailableQuantity { get; private set; }

    /// <summary>
    /// 请求令牌
    /// </summary>
    public string APIKey { get; private set; }

    public Guid Creator { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid Modifier { get; set; }

    public DateTime ModificationTime { get; set; }

    protected ChatApplicationShare()
    {
    }

    public ChatApplicationShare(string name, string chatApplicationId, DateTime? expires,
        long availableToken, int availableQuantity)
    {
        Name = name;
        ChatApplicationId = chatApplicationId;
        Expires = expires;
        AvailableToken = availableToken;
        AvailableQuantity = availableQuantity;
        UpdateApiKey();
    }

    public void UpdateApiKey()
    {
        APIKey = "sk-" + StringHelper.GenerateRandomString(38);
    }

    public void UseToken(int quantity)
    {
        if (AvailableToken == -1)
        {
            return;
        }

        if (AvailableToken < quantity)
        {
            throw new UserFriendlyException("Token不足");
        }

        AvailableToken -= quantity;
        UsedToken += quantity;
    }
}