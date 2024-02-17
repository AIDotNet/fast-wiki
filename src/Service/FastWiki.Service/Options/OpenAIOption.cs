namespace FastWiki.Service;

public class OpenAIOption
{
    public const string Name = "OpenAI";
    
    public static string ChatEndpoint { get; set; }
    
    public static string EmbeddingEndpoint { get; set; }
    
    public static string ChatToken { get; set; }
    
    public static string ChatModel { get; set; } = "gpt-3.5-turbo";
    
    public static string EmbeddingModel { get; set; } = "text-embedding-3-small";
}