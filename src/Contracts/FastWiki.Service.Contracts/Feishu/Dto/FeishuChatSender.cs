namespace FastWiki.Service.Contracts.Model.Dto;

public class FeishuChatSender
{
    public FeishuChatSenderId sender_id { get; set; }
    public string sender_type { get; set; }
    public string tenant_key { get; set; }
}