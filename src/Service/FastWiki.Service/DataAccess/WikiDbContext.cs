using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Domain.Users.Aggregates;

namespace FastWiki.Service.DataAccess;

public class WikiDbContext(MasaDbContextOptions<WikiDbContext> options) : MasaDbContext(options)
{
    public DbSet<Wiki> Wikis { get; set; }

    public DbSet<FileStorage> FileStorages { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {
        base.OnModelCreatingExecuting(modelBuilder);
        ConfigEntities(modelBuilder);
    }

    private static void ConfigEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wiki>(entity =>
        {
            entity.ToTable("wiki-wikis");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Name).HasMaxLength(100);


            entity.HasIndex(e => e.Name);
        });

        modelBuilder.Entity<FileStorage>(entity =>
        {
            entity.ToTable("wiki-file_storages");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Path).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("wiki-users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        var user = new User("admin", "admin", "Aa123456",
            "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", "239573049@qq.com", "13049809673", false);
        
        // 默认初始账号
        modelBuilder.Entity<User>().HasData(user);
        
        
    }
}