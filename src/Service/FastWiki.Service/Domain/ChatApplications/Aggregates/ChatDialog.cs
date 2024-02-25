namespace FastWiki.Service.Domain.ChatApplications.Aggregates;

public sealed class ChatDialog : Entity<string> , IFullAggregateRoot<Guid>
{
    public string Name { get; set; }

    public long WikiId { get; set; }

    public string Description { get; set; }

    public Guid Creator { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid Modifier { get; set; }

    public DateTime ModificationTime { get; set; }

    public bool IsDeleted { get; set; }

    protected ChatDialog()
    {

    }

    public ChatDialog(string name, long wikiId, string description)
    {
        Name = name;
        WikiId = wikiId;
        Description = description;
    }
}
