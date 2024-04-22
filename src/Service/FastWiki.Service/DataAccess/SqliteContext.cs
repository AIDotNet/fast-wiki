namespace FastWiki.Service.DataAccess;

public class SqliteContext(MasaDbContextOptions<WikiDbContext> options) : WikiDbContext(options)
{
    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {
        base.OnModelCreatingExecuting(modelBuilder);
    }
}