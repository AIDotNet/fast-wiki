namespace FastWiki.Service.Contracts.Wikis;

public class SubsectionInput(string content, string fileId, string name)
{
    public string FileId { get; set; } = fileId;

    public string Name { get; set; } = name;

    public string Content { get; set; } = content;
}