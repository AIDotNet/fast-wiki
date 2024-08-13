namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public class QuestionsDto
{
    public string Id { get; set; }

    /// <summary>
    ///     应用
    /// </summary>
    public string ApplicationId { get; set; }

    /// <summary>
    ///     提问内容
    /// </summary>
    public string Question { get; set; }

    /// <summary>
    ///     热度
    /// </summary>
    public int Order { get; set; }
}