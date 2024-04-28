using Masa.BuildingBlocks.Ddd.Domain.Entities.Full;

namespace FastWiki.Service.Entities;

/// <summary>
/// AI function
/// </summary>
public class AIFunction : FullAggregateRoot<long, Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }

    /// <summary>
    /// function call content（js function）
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 入场参数描述
    /// </summary>
    public List<AIFunctionItem> Parameters { get; set; }

    /// <summary>
    /// 当前function call的变量
    /// </summary>
    public List<AIFunctionItem> Items { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// 引用的库
    /// </summary>
    public List<string> Imports { get; set; }

    /// <summary>
    /// 进入function call的主函数
    /// </summary>
    public string Main { get; set; }
    
    protected AIFunction()
    {
    }
    
    public AIFunction(string name, string description, string content, List<AIFunctionItem> parameters, List<AIFunctionItem> items, List<string> imports, string main)
    {
        Name = name;
        Description = description;
        Content = content;
        Parameters = parameters;
        Items = items;
        Imports = imports;
        Main = main;
    }
}

public class AIFunctionItem
{
    public string Key { get; set; }

    public string Value { get; set; }
}