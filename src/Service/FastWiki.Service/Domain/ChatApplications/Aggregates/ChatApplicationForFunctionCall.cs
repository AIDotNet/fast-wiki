namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

public sealed class ChatApplicationForFunctionCall : Entity<long>
{
    public long FunctionCallId { get; set; }

    public string ChatApplicationId { get; set; }
}