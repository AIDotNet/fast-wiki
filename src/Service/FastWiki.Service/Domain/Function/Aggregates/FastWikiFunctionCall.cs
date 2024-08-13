namespace FastWiki.Service.Domain.Function.Aggregates;

public sealed class FastWikiFunctionCall : FullAggregateRoot<long, Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }

    /// <summary>
    ///     function call content（js function）
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     入场参数描述
    /// </summary>
    public List<FunctionItem> Parameters { get; set; }

    /// <summary>
    ///     当前function call的变量
    /// </summary>
    public List<FunctionItem> Items { get; set; }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    ///     引用的库
    /// </summary>
    public List<string> Imports { get; set; }

    /// <summary>
    ///     进入function call的主函数
    /// </summary>
    public string Main { get; set; }
}

public class FunctionItem
{
    public string Key { get; set; }

    public string Value { get; set; }
}