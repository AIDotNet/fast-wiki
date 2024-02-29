namespace FastWiki.Service.Application.ChatApplications.Queries;

/// <summary>
/// 通过分享对话id获取应用
/// </summary>
/// <param name="chatSharedId"></param>
public record ChatShareApplicationQuery(string chatSharedId) : Query<ChatApplicationDto>
{
    public override ChatApplicationDto Result
    {
        get;
        set;
    }
}