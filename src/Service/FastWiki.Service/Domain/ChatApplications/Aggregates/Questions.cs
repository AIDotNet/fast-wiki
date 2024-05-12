using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

/// <summary>
/// 问题热点
/// </summary>
public class Questions : Entity<string>, IAuditAggregateRoot<Guid>
{
    /// <summary>
    /// 应用
    /// </summary>
    public string ApplicationId { get; set; }
   
    /// <summary>
    /// 提问内容
    /// </summary>
    public string Question { get; set; }

    /// <summary>
    /// 热度
    /// </summary>
    public int Order { get; set; }
    
    public Guid Creator { get; }
    
    public DateTime CreationTime { get; }
    
    public Guid Modifier { get; }
    
    public DateTime ModificationTime { get; }
}