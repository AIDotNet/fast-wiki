namespace FastWiki.Service.Application.ChatApplications.Queries;

public record ChatApplicationInfoQuery(string Id) : Query<ChatApplicationDto>
{
    public override ChatApplicationDto Result
    {
        get;
        set;
    }
}