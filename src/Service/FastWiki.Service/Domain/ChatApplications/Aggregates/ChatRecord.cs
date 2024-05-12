using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

public sealed class ChatRecord : Entity<string>, IAuditAggregateRoot<Guid>
{
    /// <summary>
    /// 应用
    /// </summary>
    public string ApplicationId { get; set; }

    /// <summary>
    /// 提问内容
    /// </summary>
    public string Question { get; set; }
    
    public Guid Creator { get; set; }
    
    public DateTime CreationTime { get;set; }

    public Guid Modifier { get; set;}
    
    public DateTime ModificationTime { get;set; }
}