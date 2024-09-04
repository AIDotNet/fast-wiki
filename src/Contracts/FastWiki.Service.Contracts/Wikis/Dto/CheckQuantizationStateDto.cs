namespace FastWiki.Service.Contracts.Wikis.Dto;

public class CheckQuantizationStateDto
{
    /// <summary>
    ///     文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    ///     知识库状态
    /// </summary>
    public QuantizedListState State { get; set; }

    public string StateName
    {
        get
        {
            return State switch
            {
                QuantizedListState.Pending => "处理中",
                QuantizedListState.Fail => "处理失败",
                QuantizedListState.Success => "处理成功",
                _ => "未知"
            };
        }
    }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? ProcessTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }
}