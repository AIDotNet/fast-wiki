using FastWiki.Service.Domain.Storage.Aggregates;
using System.Text.Json;
using AIDotNet.OpenAI;
using AIDotNet.SparkDesk;
using FastWiki.Service.Domain.Model.Aggregates;

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

    public DbSet<ChatShare> ChatShares { get; set; }

    public DbSet<FastModel> FastModels { get; set; }

    public DbSet<ModelLogger> ModelLoggers { get; set; }

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
        });

        modelBuilder.Entity<ChatDialog>(entity =>
        {
            entity.ToTable("wiki-chat-dialog");

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.ChatId);
        });

        modelBuilder.Entity<ChatDialogHistory>(entity =>
        {
            entity.ToTable("wiki-chat-dialog-history");

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.ChatDialogId);
            entity.HasIndex(x => x.Creator);
            entity.Property(x => x.Content).HasMaxLength(-1);

            entity.Property(x => x.ReferenceFile)
                .HasConversion(item => JsonSerializer.Serialize(item, new JsonSerializerOptions()),
                    item => JsonSerializer.Deserialize<List<ReferenceFile>>(item, new JsonSerializerOptions()));
        });

        modelBuilder.Entity<ChatShare>(entity =>
        {
            entity.ToTable("wiki-chat-share");

            entity.HasKey(x => x.APIKey);

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.ChatApplicationId);
        });

        modelBuilder.Entity<FastModel>(entity =>
        {
            entity.ToTable("wiki-fast-models");
            entity.HasKey(e => e.Id);

            entity.HasIndex(x => x.Name);
            entity.HasIndex(x => x.Type);

            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Type).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(200);
            entity.Property(e => e.ApiKey).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Models).HasMaxLength(-1);

            entity.Property(x => x.Models)
                .HasConversion(item => JsonSerializer.Serialize(item, new JsonSerializerOptions()),
                    item => JsonSerializer.Deserialize<List<string>>(item, new JsonSerializerOptions()));
        });

        modelBuilder.Entity<ModelLogger>(entity =>
        {
            entity.ToTable("wiki-model-logger");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasIndex(x => x.FastModelId);
            entity.HasIndex(x => x.UserId);
            entity.HasIndex(x => x.ApplicationId);
            entity.HasIndex(x => x.ApiKey);
            entity.HasIndex(x => x.Type);
            entity.HasIndex(x => x.CreationTime);
        });

        var user = new User("admin", "admin", "Aa123456",
            "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", "239573049@qq.com", "13049809673", false);

        user.SetAdminRole();

        // 默认初始账号
        modelBuilder.Entity<User>().HasData(user);

        var openAI = new FastModel("OpenAI", OpenAIOptions.ServiceName, "https://api.openai.com/", string.Empty,
            "OpenAI", new List<string>()
            {
                "gpt-3.5-turbo",
                "gpt-3.5-turbo-0125",
                "gpt-3.5-turbo-1106",
                "gpt-3.5-turbo-16k",
                "gpt-3.5-turbo-0613",
                "gpt-3.5-turbo-16k-0613",
                "gpt-4-0125-preview",
                "gpt-4-turbo-preview",
                "gpt-4-1106-preview",
                "gpt-4-vision-preview",
                "gpt-4-1106-vision-preview",
                "gpt-4",
                "gpt-4-0613",
                "gpt-4-32k",
                "gpt-4-32k-0613"
            }, 1);

        var sparkDesk = new FastModel("SparkDesk", SparkDeskOptions.ServiceName, "", string.Empty, "星火大模型",
        [
            "SparkDesk-v3.5",
            "SparkDesk-v3.1",
            "SparkDesk-v1.5",
            "SparkDesk-v2.1"
        ], 1);

        modelBuilder.Entity<FastModel>().HasData(openAI, sparkDesk);
    }
}