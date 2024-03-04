namespace FastWiki.Service.Contracts.ChatApplication.Dto;

/// <summary>
/// 源文件项
/// </summary>
public sealed class SourceFileItem
{
    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件内容
    /// </summary>
    public string Content { get; set; }
}