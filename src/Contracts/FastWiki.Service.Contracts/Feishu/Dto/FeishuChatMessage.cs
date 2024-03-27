namespace FastWiki.Service.Contracts.Model.Dto;

public sealed class FeishuChatMessage
{
    public string message_id { get; set; }
    public string root_id { get; set; }
    public string parent_id { get; set; }
    public string create_time { get; set; }
    public string update_time { get; set; }
    public string chat_id { get; set; }
    public string chat_type { get; set; }
    public string message_type { get; set; }
    public string content { get; set; }
    public FeishuChatMention[] mentions { get; set; }
    public string user_agent { get; set; }
}