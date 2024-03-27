namespace FastWiki.Service.Contracts.Model.Dto;

public sealed class FeishuChatMention
{
    public string key { get; set; }
    public FeishuChatId id { get; set; }
    public string name { get; set; }
    public string tenant_key { get; set; }
}