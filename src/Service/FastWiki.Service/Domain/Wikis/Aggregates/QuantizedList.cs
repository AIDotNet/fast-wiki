namespace FastWiki.Service.Domain.Wikis.Aggregates;

/// <summary>
/// 量化列表
/// </summary>
public sealed class QuantizedList : Entity<long>
{
    /// <summary>
    /// 具体的知识库Id
    /// </summary>
    public long WikiId { get; set; }

    /// <summary>
    /// 具体的知识库详情Id
    /// </summary>
    public long WikiDetailId { get; set; }

    /// <summary>
    /// 处理状态
    /// </summary>
    public QuantizedListState State { get; set; }

    /// <summary>
    /// 处理备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? ProcessTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }
}