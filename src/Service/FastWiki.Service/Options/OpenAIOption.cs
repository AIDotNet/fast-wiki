namespace FastWiki.Service;

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
    /// 对话模型
    /// </summary>
    public static string ChatModel { get; set; } = "gpt-3.5-turbo";
}