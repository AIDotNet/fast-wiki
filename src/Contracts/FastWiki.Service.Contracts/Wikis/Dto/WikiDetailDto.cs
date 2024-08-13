namespace FastWiki.Service.Contracts.Wikis.Dto;

public sealed class WikiDetailDto
{
    public long Id { get; set; }

    /// <summary>
    ///     知识库Id
    /// </summary>
    public long WikiId { get; set; }

    /// <summary>
    ///     文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    ///     文件路径 如果文件类型是链接，则为链接地址，否则为文件路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    ///     数据数量
    /// </summary>
    public int DataCount { get; set; }

    /// <summary>
    ///     知识库文件类型
    /// </summary>
    public string Type { get; set; }

    public long Creator { get; }

    public DateTime CreationTime { get; set; }
    public WikiQuantizationState State { get; set; }

    public string StateName
    {
        get
        {
            switch (State)
            {
                case WikiQuantizationState.None:
                    return "处理中";
                case WikiQuantizationState.Accomplish:
                    return "已完成";
                case WikiQuantizationState.Fail:
                    return "失败";
            }

            return "错误状态";
        }
    }

    public long Modifier { get; set; }

    public DateTime ModificationTime { get; set; }
}