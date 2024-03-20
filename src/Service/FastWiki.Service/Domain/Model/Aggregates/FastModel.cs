namespace FastWiki.Service.Domain.Model.Aggregates;

public sealed class FastModel : FullAggregateRoot<string, Guid?>
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

    /// <summary>
    /// 测试时间
    /// </summary>
    public long? TestTime { get; set; }

    /// <summary>
    /// 已消耗配额
    /// </summary>
    public long UsedQuota { get; set; }

    /// <summary>
    /// 启用
    /// </summary>
    public bool Enable { get; private set; }

    protected FastModel()
    {
    }

    public FastModel(string name, string type, string url, string apiKey, string description, List<string> models,
        int order)
    {
        Id = Guid.NewGuid().ToString("N");
        Name = name;
        Type = type;
        Url = url;
        ApiKey = apiKey;
        Description = description;
        Models = models;
        Order = order;
        TestTime = null;
        SetEnable(true);
    }

    public void SetEnable(bool enable)
    {
        Enable = enable;
    }
}