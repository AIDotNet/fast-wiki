using System.Text.Json.Serialization;

namespace FastWiki.Service.Contracts.Model.Dto;

public sealed class FeishuChatInput
{
    public string schema { get; set; }
    public FeishuChatHeader header { get; set; }

    [JsonPropertyName("event")]
    public FeishuChatEvent _event { get; set; }

    public string? challenge { get; set; }
    public string? encrypt { get; set; }

    public string? type { get; set; }
}