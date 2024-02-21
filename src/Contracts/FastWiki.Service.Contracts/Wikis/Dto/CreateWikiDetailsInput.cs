namespace FastWiki.Service.Contracts.Wikis.Dto;

public  sealed class CreateWikiDetailsInput
{
    public long WikiId { get; set; }

    public string Name { get; set; }

    public long FileId { get; set; }

    public string FilePath { get; set; }

    public IEnumerable<string> Lins { get; set; }
}