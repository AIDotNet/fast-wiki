namespace FastWiki.Service.Contracts.Wikis.Dto;

public class SearchVectorQuantityDto
{
    public string? Content { get; set; }

    public string? FullPath { get; set; }

    public string? FileName { get; set; }

    /// <summary>
    /// 语义检索
    /// </summary>
    public double Relevance { get; set; }
    
    public string DocumentId { get; set; }

    /// <summary>
    /// 文件Id
    /// </summary>
    public string FileId { get; set; }
}