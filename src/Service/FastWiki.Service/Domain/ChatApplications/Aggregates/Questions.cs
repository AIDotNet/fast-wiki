namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

/// <summary>
/// 问题热点
/// </summary>
public class Questions : Entity<string>
{
    /// <summary>
    /// 应用
    /// </summary>
    public string ApplicationId { get; set; }
   
    /// <summary>
    /// 提问内容
    /// </summary>
    public string Question { get; set; }

    /// <summary>
    /// 热度
    /// </summary>
    public int Order { get; set; }
    
    public DateTime CreationTime { get;set; }
    
    protected Questions()
    {
    }
    
    public Questions(string id, string applicationId, string question, int order)
    {
        Id = id;
        ApplicationId = applicationId;
        Question = question;
        Order = order;
        CreationTime = DateTime.Now;
    }
}