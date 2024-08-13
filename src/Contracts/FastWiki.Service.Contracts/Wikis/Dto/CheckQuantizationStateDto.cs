namespace FastWiki.Service.Contracts.Wikis.Dto;

public class CheckQuantizationStateDto
{
    /// <summary>
    ///     知识库Id
    /// </summary>
    public long WikiId { get; set; }

    /// <summary>
    ///     文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    ///     知识库状态
    /// </summary>
    public WikiQuantizationState State { get; set; }
}