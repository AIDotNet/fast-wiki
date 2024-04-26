namespace FastWiki.Service.Contracts.Model.Dto;

public sealed class FeishuChatEvent
{
    public FeishuChatSender sender { get; set; }
    public FeishuChatMessage message { get; set; }
}