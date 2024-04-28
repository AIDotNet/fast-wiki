namespace FastWiki.Service.Options;

public class OpenAIOption
{
    public const string Name = "OpenAI";

    /// <summary>
    /// 对话模型的 API Endpoint
    /// </summary>
    public static string ChatEndpoint { get; set; }

    /// <summary>
    /// 量化模型的 API Endpoint
    /// </summary>
    public static string EmbeddingEndpoint { get; set; }

    /// <summary>
    /// 对话模型的 API Key
    /// </summary>
    public static string ChatToken { get; set; }

    /// <summary>
    /// 量化模型的 API Key
    /// </summary>
    public static string EmbeddingToken { get; set; }
    
    /// <summary>
    /// 量化模型
    /// </summary>
    public static string EmbeddingModel { get; set; } = "text-embedding-3-small";
}