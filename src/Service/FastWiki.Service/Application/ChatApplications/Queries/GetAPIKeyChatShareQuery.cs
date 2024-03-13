namespace FastWiki.Service.Application.ChatApplications.Queries;

public record GetAPIKeyChatShareQuery(string APIKey):Query<ChatShare>
{
    public override ChatShare Result { get; set; }
}