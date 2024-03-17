namespace FastWiki.Service.Application.ChatApplications.Queries;

public record GetAPIKeyChatShareQuery(string APIKey):Query<ChatShareDto>
{
    public override ChatShareDto Result { get; set; }
}