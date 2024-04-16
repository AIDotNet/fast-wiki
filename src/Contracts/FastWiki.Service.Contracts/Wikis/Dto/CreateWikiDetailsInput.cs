namespace FastWiki.Service.Contracts.Wikis.Dto;

public sealed class CreateWikiDetailsInput
{
    public long WikiId { get; set; }

    public string Name { get; set; }

    public long FileId { get; set; }

    public string FilePath { get; set; }

    public int MaxTokensPerParagraph { get; set; }

    public int MaxTokensPerLine { get; set; }

    public int OverlappingTokens { get; set; }
    
    public ProcessMode Mode { get; set; } = ProcessMode.Auto;

    public TrainingPattern TrainingPattern { get; set; } = TrainingPattern.Subsection;
    
    public string? QAPromptTemplate { get; set; }
}