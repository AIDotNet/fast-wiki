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
    /// 知识库模型
    /// </summary>
    public string Model { get; set; }
}