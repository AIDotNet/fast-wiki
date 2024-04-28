using Masa.BuildingBlocks.Ddd.Domain.Entities.Full;

namespace FastWiki.Service.Entities;

public class Wiki : FullAggregateRoot<long, Guid?>
{
    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; private set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 知识库QA问答模型
    /// </summary>
    public string ChatModel { get; private set; }

    /// <summary>
    /// 知识库嵌入模型
    /// </summary>
    public string EmbeddingModel { get; private set; }

    protected Wiki()
    {
    }

    public Wiki(string name, string icon, string chatModel, string embeddingModel)
    {
        Name = name;
        Icon = icon;
        ChatModel = chatModel;
        EmbeddingModel = embeddingModel;
    }
}