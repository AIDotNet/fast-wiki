using System.Text.Json.Serialization;

namespace FastWiki.Service.Contracts.Feishu.Dto;

public sealed class FeiShuChatToken
{
    [JsonPropertyName("tenant_access_token")]
    public string TenantAccessToken { get; set; }

    [JsonPropertyName("user_access_token")]
    public string UserAccessToken { get; set; }

    [JsonPropertyName("expire")] public int Expire { get; set; }
}