namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public class ChatApplicationDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// 提示词
    /// </summary>
    public string Prompt { get; set; }

    /// <summary>
    /// 对话模型
    /// </summary>
    public string ChatModel { get; set; }

    /// <summary>
    /// 温度
    /// </summary>
    public double Temperature { get; set; }

    /// <summary>
    /// 最大响应Token数量
    /// </summary>
    public int MaxResponseToken { get; set; }

    /// <summary>
    /// 模板
    /// </summary>
    public string Template { get; set; }

    /// <summary>
    /// 参数
    /// </summary>
    public Dictionary<string, string> Parameter { get; set; }

    /// <summary>
    /// 开场白
    /// </summary>
    public string Opener { get; set; }

    /// <summary>
    /// 关联的知识库
    /// </summary>
    public List<long> WikiIds { get; set; }
    
    
    /// <summary>
    /// 引用上限
    /// </summary>
    public int ReferenceUpperLimit { get; set; } = 1500;

    /// <summary>
    /// 未找到的回答模板
    /// 如果模板为空则使用Chat对话模型回答。
    /// </summary>
    public string? NoReplyFoundTemplate { get; set; }
    
    /// <summary>
    /// 显示引用文件
    /// </summary>
    public bool ShowSourceFile { get; set; }
    
    /// <summary>
    /// 匹配相似度
    /// </summary>
    public double Relevancy { get; set; } = 0.4;

    /// <summary>
    /// AI模型类型
    /// </summary>
    public string ChatType { get; set; }
}