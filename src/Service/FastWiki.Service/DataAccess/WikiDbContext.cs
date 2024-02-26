using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Domain.Users.Aggregates;
using System.Text.Json;

namespace FastWiki.Service.DataAccess;

public class WikiDbContext(MasaDbContextOptions<WikiDbContext> options) : MasaDbContext(options)
{
    public DbSet<Wiki> Wikis { get; set; }

    public DbSet<FileStorage> FileStorages { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<WikiDetail> WikiDetails { get; set; }

    public DbSet<ChatApplication> ChatApplications { get; set; }

    public DbSet<ChatDialog> ChatDialogs { get; set; }

    public DbSet<ChatDialogHistory> ChatDialogHistorys { get; set; }

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

        modelBuilder.Entity<WikiDetail>(entity =>
        {
            entity.ToTable("wiki-wiki_details");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.Path).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(100);
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

        modelBuilder.Entity<ChatApplication>(entity =>
        {
            entity.ToTable("wiki-chat-application");

            entity.HasKey(e => e.Id);

            entity.HasIndex(x => x.Name);

            entity.Property(x => x.Parameter)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()));

            entity.Property(x => x.WikiIds)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<List<long>>(v, new JsonSerializerOptions()));
        });

        modelBuilder.Entity<ChatDialog>(entity =>
        {
            entity.ToTable("wiki-chat-dialog");

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.ChatApplicationId);
        });

        modelBuilder.Entity<ChatDialogHistory>(entity =>
        {
            entity.ToTable("wiki-chat-dialog-history");

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.ChatApplicationId);
            entity.HasIndex(x => x.ChatDialogId);
            entity.HasIndex(x => x.Creator);
            entity.Property(x => x.Content).HasMaxLength(-1);

        });

        var user = new User("admin", "admin", "Aa123456",
            "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", "239573049@qq.com", "13049809673", false);

        // 默认初始账号
        modelBuilder.Entity<User>().HasData(user);
    }
}