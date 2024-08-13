namespace FastWiki.Service.Application.ChatApplications.Queries;

/// <summary>
///     获取分享对话详情信息
/// </summary>
/// <param name="Id"></param>
public record ChatShareInfoQuery(string Id) : Query<ChatShareDto>
{
    public override ChatShareDto Result { get; set; }
}