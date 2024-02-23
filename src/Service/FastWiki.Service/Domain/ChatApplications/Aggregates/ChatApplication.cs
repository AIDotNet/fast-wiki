namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

/// <summary>
/// 对话应用
/// </summary>
public sealed class ChatApplication : FullAggregateRoot<string,Guid?>
{
    protected ChatApplication()
    {
    }

    public ChatApplication(string id) : base(id)
    {
        Opener = 
"""
您好，欢迎使用FastWiki知识库，我可以帮助您查找知识库中的内容，您可以输入您的问题，我会尽力帮您解答。
来自FastWiki知识库的问候。                 
""";
    }

    public string Name { get; set; }

    /// <summary>
    /// 提示词
    /// </summary>
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// 对话模型
    /// </summary>
    public string ChatModel { get; set; } = "gpt-3.5-turbo";

    /// <summary>
    /// 温度
    /// </summary>
    public int Temperature { get; set; } = 0;

    /// <summary>
    /// 最大响应Token数量
    /// </summary>
    public int MaxResponseToken { get; set; } = 2000;

    /// <summary>
    /// 模板
    /// </summary>
    public string Template { get; set; } = string.Empty;

    /// <summary>
    /// 参数
    /// </summary>
    public Dictionary<string,string> Parameter { get; set; } = new();

    public string Opener { get; set; }

    /// <summary>
    /// 关联的知识库
    /// </summary>
    public List<long> WikiIds { get; set; } = new();
}