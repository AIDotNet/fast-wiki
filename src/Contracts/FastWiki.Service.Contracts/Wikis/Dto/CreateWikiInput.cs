namespace FastWiki.Service.Contracts.Wikis.Dto;

public sealed class CreateWikiInput
{
    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 知识库QA问答模型
    /// </summary>
    public string Model { get; set; } = "gpt-3.5-turbo";

    /// <summary>
    /// 知识库向量化模型
    /// </summary>
    public string EmbeddingModel { get; set; } = "text-embedding-3-small";
}