namespace FastWiki.Service.Contracts.Wikis.Dto;

public sealed class CreateWikiDetailsInput
{
    public long WikiId { get; set; }

    public string Name { get; set; }

    public long FileId { get; set; }

    public string FilePath { get; set; }

    public int Subsection { get; set; }

    public ProcessMode Mode { get; set; } = ProcessMode.Auto;

    public TrainingPattern TrainingPattern { get; set; } = TrainingPattern.Subsection;
}