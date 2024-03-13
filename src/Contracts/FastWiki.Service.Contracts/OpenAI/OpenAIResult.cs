using System.Text.Json.Serialization;

namespace FastWiki.Service.Contracts.OpenAI;


public sealed class OpenAIResult
{
    public string id { get; set; }
    
    [JsonPropertyName("object")]
    public string _object { get; set; }
    
    public string model { get; set; }
    
    public string system_fingerprint { get; set; }
    
    public long created { get; set; }
    
    public Choice[] choices { get; set; }
    
    public Usage usage { get; set; }

    public OpenAIError error { get; set; }
}


public class Usage
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}

public class Choice
{
    public int index { get; set; }

    public MessageDto message { get; set; }

    public MessageDto delta { get; set; }

    public string? finish_reason { get; set; } = null;
    
    public string? logprobs { get; set; } = null;
}

public class MessageDto
{
    public string role { get; set; }
    public string content { get; set; }
}
