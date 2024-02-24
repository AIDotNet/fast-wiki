namespace FastWiki.Service.Contracts.Wikis.Dto;

public sealed class WikiDetailVectorQuantityDto
{
    public string Id { get; set; }

    /// <summary>
    /// 向量原始内容
    /// </summary>
    public string Content { get; set; }

    public string Document_Id { get; set; }

    /// <summary>
    /// 关联知识库文件Id
    /// </summary>
    public string WikiDetailId { get; set; }

    /// <summary>
    /// 关联文件Id
    /// </summary>
    public string FileId { get; set; }
}