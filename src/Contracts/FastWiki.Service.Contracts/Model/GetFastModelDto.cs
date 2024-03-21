namespace FastWiki.Service.Contracts.Model;

public class GetFastModelDto
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    /// <summary>
    /// 模型类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// AI支持的模型
    /// </summary>
    public List<string> Models { get; set; } = [];
}