namespace FastWiki.Service.Domain.Wikis.Aggregates;

public sealed class WikiDetailsDocument : Entity<long>
{
    public long WikiDetailsId { get; set; }

    public string DocumentId { get; set; }
}