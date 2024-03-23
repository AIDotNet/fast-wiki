namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public sealed class CreateChatDialogHistoryInput 
{
    public string? Id { get; set; }
    
    /// <summary>
    /// 对话id
    /// </summary>
    public string ChatDialogId { get; set; }

    /// <summary>
    /// 对话内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否本人
    /// </summary>
    public bool Current { get; set; }

    /// <summary>
    /// 对话类型
    /// </summary>
    public ChatDialogHistoryType Type { get; set; }

    /// <summary>
    /// 消耗token
    /// </summary>
    public int ExpendToken { get; set; }
    
    /// <summary>
    /// 源文件引用
    /// </summary>
    public List<SourceFileDto> ReferenceFile { get; set; } = [];
}