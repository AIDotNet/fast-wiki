namespace FastWiki.Service.Contracts.Function.Dto;

public sealed class FastWikiFunctionCallDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    /// <summary>
    ///     function call content（js function）
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     入场参数描述
    /// </summary>
    public List<FunctionItemDto> Parameters { get; set; }

    /// <summary>
    ///     当前function call的变量
    /// </summary>
    public List<FunctionItemDto> Items { get; set; }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    ///     引用的库
    /// </summary>
    public List<string> Imports { get; set; }

    public DateTime CreationTime { get; set; }

    /// <summary>
    ///     进入function call的主函数
    /// </summary>
    public string Main { get; set; }
}

public sealed class FunctionItemDto
{
    public string Key { get; set; }

    public string Value { get; set; }
}