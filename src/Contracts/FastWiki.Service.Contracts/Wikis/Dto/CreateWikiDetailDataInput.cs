namespace FastWiki.Service.Contracts.Wikis.Dto;

public sealed class CreateWikiDetailDataInput
{
    /// <summary>
    /// 知识库Id
    /// </summary>
    public long WikiId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    public long FileId { get; set; }

    public string FilePath { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string State { get; set; }

    public int MaxTokensPerParagraph { get; set; }

    public int MaxTokensPerLine { get; set; }

    public int OverlappingTokens { get; set; }


    public ProcessMode Mode { get; set; } = ProcessMode.Auto;

    public TrainingPattern TrainingPattern { get; set; } = TrainingPattern.Subsection;
}