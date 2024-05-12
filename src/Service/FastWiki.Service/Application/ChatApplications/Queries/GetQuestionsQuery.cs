namespace FastWiki.Service.Application.ChatApplications.Queries;

public record GetQuestionsQuery(string ApplicationId) : Query<List<QuestionsDto>>
{
    public override List<QuestionsDto> Result { get; set; }
}