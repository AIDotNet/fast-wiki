namespace FastWiki.Service.Domain.Wikis.Aggregates;

public sealed class Wiki : FullAggregateRoot<long, Guid?>
{
    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 知识库模型
    /// </summary>
    public string Model { get; set; }
    
    protected Wiki(){}
    
    public Wiki(string icon, string name, string model)
    {
        Icon = icon;
        Name = name;
        Model = model;
    }
}