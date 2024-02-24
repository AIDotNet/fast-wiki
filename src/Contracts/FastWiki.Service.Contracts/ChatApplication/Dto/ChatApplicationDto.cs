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

    public string Opener { get; set; }

    /// <summary>
    /// 关联的知识库
    /// </summary>
    public List<long> WikiIds { get; set; }
}