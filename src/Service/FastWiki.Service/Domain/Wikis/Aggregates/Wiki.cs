namespace FastWiki.Service.Domain.Wikis.Aggregates;

public sealed class Wiki : FullAggregateRoot<long, Guid?>
{
    public Wiki()
    {
    }

    public Wiki(string icon, string name, string model, string embeddingModel, VectorType vectorType)
    {
        Icon = icon;
        Name = name;
        Model = model;
        EmbeddingModel = embeddingModel;
        VectorType = vectorType;
    }

    /// <summary>
    ///     图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     知识库模型
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    ///     知识库向量化模型
    /// </summary>
    public string EmbeddingModel { get; set; }
    
    /// <summary>
    /// 向量数据库类型
    /// </summary>
    public VectorType VectorType { get; set; }
}