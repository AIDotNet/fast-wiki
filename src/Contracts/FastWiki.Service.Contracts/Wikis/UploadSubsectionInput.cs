namespace FastWiki.Service.Contracts.Wikis;

public sealed class UploadSubsectionInput
{
    public string Name { get; set; }

    public int Count { get; set; }

    public int FileProgress { get; set; }

    public int DataProgress { get; set; }

    public string Hash { get; set; }
}