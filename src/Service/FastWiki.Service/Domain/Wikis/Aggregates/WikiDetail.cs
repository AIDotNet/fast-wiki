using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace FastWiki.Service.Domain.Wikis.Aggregates;

/// <summary>
///     知识库详情
/// </summary>
public class WikiDetail : Entity<long>, IAuditEntity<long>
{
    /// <inheritdoc />
    public WikiDetail(long wikiId, string fileName, string path, long fileId, int dataCount, string type)
    {
        WikiId = wikiId;
        FileId = fileId;
        FileName = fileName;
        Path = path;
        DataCount = dataCount;
        Type = type;
        State = WikiQuantizationState.None;
    }

    protected WikiDetail()
    {
    }

    /// <summary>
    ///     知识库Id
    /// </summary>
    public long WikiId { get; set; }

    /// <summary>
    ///     文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    ///     文件路径 如果文件类型是链接，则为链接地址，否则为文件路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    ///     文件Id
    /// </summary>
    public long FileId { get; set; }

    /// <summary>
    ///     数据数量
    /// </summary>
    public int DataCount { get; set; }

    /// <summary>
    ///     知识库文件类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    ///     知识库状态
    /// </summary>
    public WikiQuantizationState State { get; set; }


    public int MaxTokensPerParagraph { get; set; }

    public int MaxTokensPerLine { get; set; }

    public int OverlappingTokens { get; set; }

    public ProcessMode Mode { get; set; } = ProcessMode.Auto;

    public TrainingPattern TrainingPattern { get; set; } = TrainingPattern.Subsection;

    /// <summary>
    ///     QAPrompt模板
    /// </summary>
    public string? QAPromptTemplate { get; set; } =
        """"
        我会给你一段文本，学习它们，并整理学习成果，要求为：
        1. 提出最多 20 个问题。
        2. 给出每个问题的答案。
        3. 答案要详细完整，答案可以包含普通文字、链接、代码、表格、公示、媒体链接等 markdown 元素。
        4. 按格式返回多个问题和答案:

        Q1: 问题。
        A1: 答案。
        Q2:
        A2:
        ……

        我的文本："""{{$input}}"""
        """";

    public long Creator { get; set; }

    public DateTime CreationTime { get; set; }

    public long Modifier { get; set; }

    public DateTime ModificationTime { get; set; }
}