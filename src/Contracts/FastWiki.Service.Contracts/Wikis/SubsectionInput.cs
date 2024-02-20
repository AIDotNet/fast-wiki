using System.Text.Json.Serialization;

namespace FastWiki.Service.Contracts.Wikis;

public class SubsectionInput
{
    public string FileId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<string> Lins { get; set; } = new();

    [JsonIgnore]
    public object Data { get; set; }
}