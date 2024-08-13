namespace FastWiki.Service.Contracts.Wikis.Dto;

public class WikiDto
{
    public long Id { get; set; }

    /// <summary>
    ///     图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     知识库模型
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    ///     知识库向量化模型
    /// </summary>
    public string EmbeddingModel { get; set; } = "text-embedding-3-small";
}