namespace FastWiki.Service.Contracts.Wikis.Dto;

public sealed class CreateWikiDetailWebPageInput
{
    /// <summary>
    /// 知识库Id
    /// </summary>
    public long WikiId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string State { get; set; }
    
    public int Subsection { get; set; }

    public ProcessMode Mode { get; set; } = ProcessMode.Auto;

    public TrainingPattern TrainingPattern { get; set; } = TrainingPattern.Subsection;
}