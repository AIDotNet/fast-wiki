namespace FastWiki.Service.Domain.Model.Aggregates;

public sealed class ModelLogger : Entity<long>
{
    public DateTime CreationTime { get; protected set; }

    /// <summary>
    /// 绑定的模型ID
    /// </summary>
    public string FastModelId { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// 应用Id
    /// </summary>
    public string? ApplicationId { get; set; }

    /// <summary>
    /// 请求Key
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// 日志类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 使用模型
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// 提示使用Token数量
    /// </summary>
    public int PromptCount { get; set; }

    /// <summary>
    /// 补全使用Token数量
    /// </summary>
    public int ComplementCount { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    protected ModelLogger()
    {
    }

    public ModelLogger(string fastModelId, Guid? userId, string? applicationId, string apiKey, string type,
        string model, int promptCount, int complementCount, string description)
    {
        FastModelId = fastModelId;
        UserId = userId;
        ApplicationId = applicationId;
        ApiKey = apiKey;
        Type = type;
        Model = model;
        PromptCount = promptCount;
        ComplementCount = complementCount;
        Description = description;
        CreationTime = DateTime.Now;
    }
}