namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

public sealed class ChatDialog : Entity<string> , IFullAggregateRoot<Guid>
{
    public string Name { get; set; }

    public string ChatApplicationId { get; set; }

    public string Description { get; set; }

    public Guid Creator { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid Modifier { get; set; }

    public DateTime ModificationTime { get; set; }

    public bool IsDeleted { get; set; }

    protected ChatDialog()
    {

    }

    public ChatDialog(string name, string chatApplicationId, string description)
    {
        Id = Guid.NewGuid().ToString("N");
        Name = name;
        ChatApplicationId = chatApplicationId;
        Description = description;
    }
}
