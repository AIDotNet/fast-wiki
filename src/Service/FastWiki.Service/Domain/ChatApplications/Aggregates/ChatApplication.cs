namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

/// <summary>
/// 对话应用
/// </summary>
public sealed class ChatApplication : FullAggregateRoot<string, Guid?>
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

        Template =
""""
使用 <data></data> 标记中的内容作为你的知识:

    <data>
    {{quote}}
    </data>

回答要求：
- 如果你不清楚答案，你需要澄清。
- 避免提及你是从 <data></data> 获取的知识。
- 保持答案与 <data></data> 中描述的一致。
- 使用 Markdown 语法优化回答格式。
- 使用与问题相同的语言回答。

问题:"""{{question}}"""
"""";
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
    public double Temperature { get; set; } = 0;

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
    public Dictionary<string, string> Parameter { get; set; } = new();

    public string Opener { get; set; }

    /// <summary>
    /// 关联的知识库
    /// </summary>
    public List<long> WikiIds { get; set; } = new();
    
    /// <summary>
    /// 引用上限
    /// </summary>
    public int ReferenceUpperLimit { get; set; } = 1500;

    /// <summary>
    /// 匹配相似度
    /// </summary>
    public double Relevancy { get; set; } = 0.4;

    /// <summary>
    /// 未找到的回答模板
    /// 如果模板为空则使用Chat对话模型回答。
    /// </summary>
    public string? NoReplyFoundTemplate { get; set; }

    /// <summary>
    /// 显示引用文件
    /// </summary>
    public bool ShowSourceFile { get; set; }
}