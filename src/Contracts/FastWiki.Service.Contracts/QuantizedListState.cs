namespace FastWiki.Service.Contracts;


/// <summary>
/// 表示量化列表的状态。
/// </summary>
public enum QuantizedListState
{
    /// <summary>
    /// 待处理状态。
    /// </summary>
    Pending = 1,

    /// <summary>
    /// 成功状态。
    /// </summary>
    Success = 2,

    /// <summary>
    /// 失败状态。
    /// </summary>
    Fail = 3
}