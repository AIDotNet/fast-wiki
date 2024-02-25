namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public class CompletionsDto
{
    public string Content { get; set; }

    /// <summary>
    /// 源文件
    /// </summary>
    public List<SourceFileDto> SourceFile { get; set; }
}

public class SourceFileDto
{
    public string Name { get; set; }

    public string FilePath { get; set; }

    public string FileId { get; set; }
}