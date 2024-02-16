using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace FastWiki.Service.Domain.Wikis.Aggregates;

public sealed class WikiDetails : Entity<long>, IAuditEntity<long>
{
    /// <summary>
    /// 知识库Id
    /// </summary>
    public long WikiId { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件路径 如果文件类型是链接，则为链接地址，否则为文件路径
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// 数据数量
    /// </summary>
    public int DataCount { get; set; }

    /// <summary>
    /// 知识库文件类型 
    /// </summary>
    public string Type { get; set; }

    public long Creator { get; }

    public DateTime CreationTime { get; set; }

    public long Modifier { get; set; }

    public DateTime ModificationTime { get; set; }
}