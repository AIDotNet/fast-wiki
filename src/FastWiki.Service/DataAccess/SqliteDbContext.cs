using Microsoft.EntityFrameworkCore;

namespace FastWiki.Service.DataAccess;

public class SqliteDbContext (MasaDbContextOptions<MasterDbContext> options) : MasterDbContext(options)
{
    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {
        base.OnModelCreatingExecuting(modelBuilder);
    }
}