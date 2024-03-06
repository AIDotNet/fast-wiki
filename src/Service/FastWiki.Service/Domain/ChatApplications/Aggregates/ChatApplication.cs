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
FastWiki本项目是一个高性能、基于最新技术栈的知识库系统，专为大规模信息检索和智能搜索设计。
    利用微软Semantic Kernel进行深度学习和自然语言处理，结合.NET 8和`MasaBlazor`前端框架，后台采用`MasaFramework`，实现了一个高效、易用、可扩展的智能向量搜索平台。
    我们的目标是提供一个能够理解和处理复杂查询的智能搜索解决方案，帮助用户快速准确地获取所需信息。采用Apache-2.0，您也可以完全商用不会有任何版权纠纷
[Github](https://github.com/239573049/fast-wiki)
[Gitee](https://gitee.com/hejiale010426/fast-wiki)
[项目文档](https://docs.token-ai.cn/)

当前AI提供了Avalonia中文文档知识库功能！  
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