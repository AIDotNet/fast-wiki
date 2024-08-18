namespace FastWiki.Service;

public class ConnectionStringsOptions
{
    public const string Name = "ConnectionStrings";

    public static string DefaultConnection { get; set; }

    public static string TableNamePrefix { get; set; }

    public static string DefaultType { get; set; }

    /// <summary>
    /// Qdrant 密钥
    /// </summary>
    public static string WikiAPIKey { get; set; }
    
    /// <summary>
    /// Wiki 连接字符串
    /// </summary>
    public static string WikiConnection { get; set; }
    
    /// <summary>
    /// QdrantPort
    /// </summary>
    public static string QdrantPort { get; set; }
    
    /// <summary>
    /// Qdrant API Key
    /// </summary>
    public static string QdrantAPIKey { get; set; }
    
    /// <summary>
    /// Qdrant Endpoint
    /// </summary>
    public static string QdrantEndpoint { get; set; }
}