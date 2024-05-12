using System.Text.Json;
using FastWiki.Service.Domain.Function.Aggregates;
using FastWiki.Service.Domain.Storage.Aggregates;

namespace FastWiki.Service.DataAccess;

/// <summary>
/// Wiki数据库上下文
/// </summary>
/// <param name="options"></param>
public class WikiDbContext(MasaDbContextOptions<WikiDbContext> options) : MasaDbContext(options)
{
    public DbSet<Wiki> Wikis { get; set; }

    public DbSet<FileStorage> FileStorages { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<WikiDetail> WikiDetails { get; set; }

    public DbSet<ChatApplication> ChatApplications { get; set; }

    public DbSet<ChatShare> ChatShares { get; set; }

    public DbSet<FastWikiFunctionCall> FunctionCalls { get; set; }

    public DbSet<ChatRecord> ChatRecords { get; set; }
    
    public DbSet<Questions> Questions { get; set; }

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
            entity.ToTable("wiki-wiki-details");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.Path).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<FileStorage>(entity =>
        {
            entity.ToTable("wiki-file-storages");
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

            entity.Property(x => x.Extend)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => v.IsNullOrEmpty()
                        ? new Dictionary<string, string>()
                        : JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()));

            entity.Property(x => x.FunctionIds)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<List<long>>(v, new JsonSerializerOptions()));
        });

        modelBuilder.Entity<ChatShare>(entity =>
        {
            entity.ToTable("wiki-chat-share");

            entity.HasKey(x => x.APIKey);

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.ChatApplicationId);
        });
        
        modelBuilder.Entity<ChatRecord>(entity =>
        {
            entity.ToTable("wiki-chat-record");

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.CreationTime);
        });
        
        modelBuilder.Entity<Questions>(entity =>
        {
            entity.ToTable("wiki-questions");

            entity.HasKey(x => x.Id);

        });


        modelBuilder.Entity<FastWikiFunctionCall>(entity =>
        {
            entity.ToTable("wiki-function-calls");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasIndex(x => x.CreationTime);

            entity.Property(x => x.Parameters)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<List<FunctionItem>>(v, new JsonSerializerOptions()));

            entity.Property(x => x.Items)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<List<FunctionItem>>(v, new JsonSerializerOptions()));


            entity.Property(x => x.Imports)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions()));
        });

        var user = new User("admin", "admin", "Aa123456",
            "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", "239573049@qq.com", "13049809673", false);

        user.SetAdminRole();

        // 默认初始账号
        modelBuilder.Entity<User>().HasData(user);
    }
}