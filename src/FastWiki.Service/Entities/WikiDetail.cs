using FastWiki.Service.Core;
using Masa.BuildingBlocks.Ddd.Domain.Entities;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace FastWiki.Service.Entities;

public sealed class WikiDetail : Entity<long>, IAuditEntity<Guid?>
{
    /// <summary>
    /// 知识库Id
    /// </summary>
    public long WikiId { get; private set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; private set; }

    /// <summary>
    /// 文件路径 如果文件类型是链接，则为链接地址，否则为文件路径
    /// </summary>
    public string Path { get; private set; }

    /// <summary>
    /// 文件Id
    /// </summary>
    public long FileId { get; private set; }

    /// <summary>
    /// 数据数量
    /// </summary>
    public int DataCount { get; private set; }

    /// <summary>
    /// 知识库文件类型 
    /// </summary>
    public string Type { get; private set; }

    /// <summary>
    /// 知识库状态
    /// </summary>
    public WikiQuantizationState State { get; private set; }

    public Guid? Creator { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid? Modifier { get; set; }

    public DateTime ModificationTime { get; set; }

    /// <summary>
    /// 每段最大令牌数
    /// </summary>
    public int MaxTokensPerParagraph { get; private set; }

    /// <summary>
    /// 最大令牌数
    /// </summary>
    public int MaxTokensPerLine { get; private set; }

    /// <summary>
    /// 重叠的令牌
    /// </summary>
    public int OverlappingTokens { get; private set; }

    /// <summary>
    /// 模式
    /// </summary>
    public ProcessMode Mode { get; private set; }

    /// <summary>
    /// 分段
    /// </summary>
    public TrainingPattern TrainingPattern { get; private set; }

    /// <summary>
    /// QAPrompt模板
    /// </summary>
    public string? QAPromptTemplate { get; private set; }

    protected WikiDetail()
    {
    }

    public WikiDetail(long wikiId, string fileName, string path, long fileId, int dataCount, string type,
        WikiQuantizationState state, int maxTokensPerParagraph, int maxTokensPerLine, int overlappingTokens,
        ProcessMode mode, TrainingPattern trainingPattern)
    {
        WikiId = wikiId;
        FileName = fileName;
        Path = path;
        FileId = fileId;
        DataCount = dataCount;
        Type = type;
        State = state;
        MaxTokensPerParagraph = maxTokensPerParagraph;
        MaxTokensPerLine = maxTokensPerLine;
        OverlappingTokens = overlappingTokens;
        Mode = mode;
        TrainingPattern = trainingPattern;
        QAPromptTemplate =
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
    }

    public void UpdateQAPromptTemplate(string template)
    {
        QAPromptTemplate = template;
    }

    public void UpdateState(WikiQuantizationState state)
    {
        State = state;
    }

    public void UpdateTrainingPattern(TrainingPattern trainingPattern)
    {
        TrainingPattern = trainingPattern;
    }

    public void UpdateMode(ProcessMode mode)
    {
        Mode = mode;
    }

    public void UpdateMaxTokensPerParagraph(int maxTokensPerParagraph)
    {
        MaxTokensPerParagraph = maxTokensPerParagraph;
    }

    public void UpdateMaxTokensPerLine(int maxTokensPerLine)
    {
        MaxTokensPerLine = maxTokensPerLine;
    }

    public void UpdateOverlappingTokens(int overlappingTokens)
    {
        OverlappingTokens = overlappingTokens;
    }

    public void UpdateDataCount(int dataCount)
    {
        DataCount = dataCount;
    }

    public void UpdateFileId(long fileId)
    {
        FileId = fileId;
    }

    public void UpdatePath(string path)
    {
        Path = path;
    }

    public void UpdateFileName(string fileName)
    {
        FileName = fileName;
    }

    public void UpdateWikiId(long wikiId)
    {
        WikiId = wikiId;
    }
}