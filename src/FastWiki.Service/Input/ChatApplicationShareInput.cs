﻿namespace FastWiki.Service.Input;

public class ChatApplicationShareInput
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get;  set; }

    /// <summary>
    /// 绑定应用
    /// </summary>
    public string ChatApplicationId { get;  set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime? Expires { get;  set; }

    /// <summary>
    /// 可用Token -1则是无限
    /// </summary>
    public long AvailableToken { get;  set; }

    /// <summary>
    /// 可用数量
    /// </summary>
    public int AvailableQuantity { get;  set; }
}