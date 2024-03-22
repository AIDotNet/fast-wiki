namespace FastWiki.Service.Contracts.Model;

public class CreateFastModeInput
{
    public string Name { get; set; }

    /// <summary>
    /// 模型类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 模型代理地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 模型密钥
    /// </summary>
    public string ApiKey { get; set; }
    
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// AI支持的模型
    /// </summary>
    public List<string> Models { get; set; } = [];

    /// <summary>
    /// 优先级
    /// </summary>
    public int Order { get; set; }
}