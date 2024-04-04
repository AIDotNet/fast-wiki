﻿namespace FastWiki.Service.Contracts.Function;

public class FastWikiFunctionCallInput
{
    
    public string Name { get; set; }

    public string Description { get; set; }

    /// <summary>
    /// function call content（js function）
    /// </summary>
    public string Cotnent { get; set; }

    /// <summary>
    /// 入场参数描述
    /// </summary>
    public Dictionary<string,string> Parameters { get; set; }
    
    /// <summary>
    /// 当前function call的变量
    /// </summary>
    public Dictionary<string,object> Items { get; set; }

    /// <summary>
    /// 引用的库
    /// </summary>
    public List<string> Imports { get; set; }
}