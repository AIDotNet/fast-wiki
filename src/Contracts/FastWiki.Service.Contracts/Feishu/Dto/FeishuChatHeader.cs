namespace FastWiki.Service.Contracts.Model.Dto;

public class FeishuChatHeader
{
    public string event_id { get; set; }
    public string event_type { get; set; }
    public string create_time { get; set; }
    public string token { get; set; }
    public string app_id { get; set; }
    public string tenant_key { get; set; }
}