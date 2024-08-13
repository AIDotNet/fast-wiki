using Microsoft.EntityFrameworkCore.Migrations;

namespace FastWiki.Service.Migrations.Sqlite;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "wiki-chat-application",
            table => new
            {
                Id = table.Column<string>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", nullable: false),
                Prompt = table.Column<string>("TEXT", nullable: false),
                ChatModel = table.Column<string>("TEXT", nullable: false),
                Temperature = table.Column<double>("REAL", nullable: false),
                MaxResponseToken = table.Column<int>("INTEGER", nullable: false),
                Template = table.Column<string>("TEXT", nullable: false),
                Parameter = table.Column<string>("TEXT", nullable: false),
                Opener = table.Column<string>("TEXT", nullable: false),
                WikiIds = table.Column<string>("TEXT", nullable: false),
                ReferenceUpperLimit = table.Column<int>("INTEGER", nullable: false),
                Relevancy = table.Column<double>("REAL", nullable: false),
                NoReplyFoundTemplate = table.Column<string>("TEXT", nullable: true),
                ShowSourceFile = table.Column<bool>("INTEGER", nullable: false),
                Extend = table.Column<string>("TEXT", nullable: false),
                FunctionIds = table.Column<string>("TEXT", nullable: false),
                Creator = table.Column<Guid>("TEXT", nullable: true),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: true),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                IsDeleted = table.Column<bool>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-application", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-chat-dialog",
            table => new
            {
                Id = table.Column<string>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", nullable: false),
                ChatId = table.Column<string>("TEXT", nullable: true),
                ApplicationId = table.Column<string>("TEXT", nullable: false),
                Description = table.Column<string>("TEXT", nullable: false),
                Type = table.Column<int>("INTEGER", nullable: false),
                Creator = table.Column<Guid>("TEXT", nullable: false),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: false),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                IsDeleted = table.Column<bool>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-dialog", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-chat-dialog-history",
            table => new
            {
                Id = table.Column<string>("TEXT", nullable: false),
                ChatDialogId = table.Column<string>("TEXT", nullable: false),
                Content = table.Column<string>("TEXT", maxLength: -1, nullable: false),
                TokenConsumption = table.Column<int>("INTEGER", nullable: false),
                Current = table.Column<bool>("INTEGER", nullable: false),
                Type = table.Column<int>("INTEGER", nullable: false),
                ReferenceFile = table.Column<string>("TEXT", nullable: false),
                Creator = table.Column<Guid>("TEXT", nullable: false),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: false),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                IsDeleted = table.Column<bool>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-dialog-history", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-chat-share",
            table => new
            {
                Id = table.Column<string>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", nullable: false),
                ChatApplicationId = table.Column<string>("TEXT", nullable: false),
                Expires = table.Column<DateTime>("TEXT", nullable: true),
                UsedToken = table.Column<long>("INTEGER", nullable: false),
                AvailableToken = table.Column<long>("INTEGER", nullable: false),
                AvailableQuantity = table.Column<int>("INTEGER", nullable: false),
                APIKey = table.Column<string>("TEXT", nullable: true),
                Creator = table.Column<Guid>("TEXT", nullable: false),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: false),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-share", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-file-storages",
            table => new
            {
                Id = table.Column<long>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>("TEXT", maxLength: 100, nullable: false),
                Path = table.Column<string>("TEXT", maxLength: 200, nullable: false),
                Size = table.Column<long>("INTEGER", nullable: false),
                IsCompression = table.Column<bool>("INTEGER", nullable: false),
                FullName = table.Column<string>("TEXT", nullable: false),
                Creator = table.Column<Guid>("TEXT", nullable: true),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: true),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                IsDeleted = table.Column<bool>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-file-storages", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-function-calls",
            table => new
            {
                Id = table.Column<long>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>("TEXT", nullable: false),
                Description = table.Column<string>("TEXT", nullable: false),
                Content = table.Column<string>("TEXT", nullable: false),
                Parameters = table.Column<string>("TEXT", nullable: false),
                Items = table.Column<string>("TEXT", nullable: false),
                Enable = table.Column<bool>("INTEGER", nullable: false),
                Imports = table.Column<string>("TEXT", nullable: false),
                Main = table.Column<string>("TEXT", nullable: false),
                Creator = table.Column<Guid>("TEXT", nullable: false),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: false),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                IsDeleted = table.Column<bool>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-function-calls", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-users",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Account = table.Column<string>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", maxLength: 100, nullable: false),
                Password = table.Column<string>("TEXT", maxLength: 100, nullable: false),
                Salt = table.Column<string>("TEXT", nullable: false),
                Avatar = table.Column<string>("TEXT", nullable: false),
                Email = table.Column<string>("TEXT", nullable: false),
                Phone = table.Column<string>("TEXT", nullable: false),
                IsDisable = table.Column<bool>("INTEGER", nullable: false),
                Role = table.Column<int>("INTEGER", nullable: false),
                Creator = table.Column<Guid>("TEXT", nullable: true),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: true),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                IsDeleted = table.Column<bool>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-users", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-wiki-details",
            table => new
            {
                Id = table.Column<long>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                WikiId = table.Column<long>("INTEGER", nullable: false),
                FileName = table.Column<string>("TEXT", maxLength: 100, nullable: false),
                Path = table.Column<string>("TEXT", maxLength: 200, nullable: false),
                FileId = table.Column<long>("INTEGER", nullable: false),
                DataCount = table.Column<int>("INTEGER", nullable: false),
                Type = table.Column<string>("TEXT", maxLength: 100, nullable: false),
                State = table.Column<int>("INTEGER", nullable: false),
                Creator = table.Column<long>("INTEGER", nullable: false),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<long>("INTEGER", nullable: false),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                MaxTokensPerParagraph = table.Column<int>("INTEGER", nullable: false),
                MaxTokensPerLine = table.Column<int>("INTEGER", nullable: false),
                OverlappingTokens = table.Column<int>("INTEGER", nullable: false),
                Mode = table.Column<int>("INTEGER", nullable: false),
                TrainingPattern = table.Column<int>("INTEGER", nullable: false),
                QAPromptTemplate = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-wiki-details", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-wikis",
            table => new
            {
                Id = table.Column<long>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Icon = table.Column<string>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", maxLength: 100, nullable: false),
                Model = table.Column<string>("TEXT", nullable: false),
                EmbeddingModel = table.Column<string>("TEXT", nullable: false),
                Creator = table.Column<Guid>("TEXT", nullable: true),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: true),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                IsDeleted = table.Column<bool>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-wikis", x => x.Id); });

        migrationBuilder.InsertData(
            "wiki-users",
            new[]
            {
                "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable",
                "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt"
            },
            new object[]
            {
                new Guid("c8b3b44a-6e7c-4f11-88b2-ed846e953b7c"), "admin",
                "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                new DateTime(2024, 4, 22, 14, 44, 40, 964, DateTimeKind.Utc).AddTicks(8730), null, "239573049@qq.com",
                false, false, new DateTime(2024, 4, 22, 14, 44, 40, 964, DateTimeKind.Utc).AddTicks(8733), null,
                "admin", "418817cad9f0eae28501500c5a6e31e9", "13049809673", 2, "06a074ae10a44f2ea7b0de027562723e"
            });

        migrationBuilder.CreateIndex(
            "IX_wiki-chat-application_Name",
            "wiki-chat-application",
            "Name");

        migrationBuilder.CreateIndex(
            "IX_wiki-chat-dialog_ChatId",
            "wiki-chat-dialog",
            "ChatId");

        migrationBuilder.CreateIndex(
            "IX_wiki-chat-dialog-history_ChatDialogId",
            "wiki-chat-dialog-history",
            "ChatDialogId");

        migrationBuilder.CreateIndex(
            "IX_wiki-chat-dialog-history_Creator",
            "wiki-chat-dialog-history",
            "Creator");

        migrationBuilder.CreateIndex(
            "IX_wiki-chat-share_ChatApplicationId",
            "wiki-chat-share",
            "ChatApplicationId");

        migrationBuilder.CreateIndex(
            "IX_wiki-function-calls_CreationTime",
            "wiki-function-calls",
            "CreationTime");

        migrationBuilder.CreateIndex(
            "IX_wiki-wikis_Name",
            "wiki-wikis",
            "Name");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "wiki-chat-application");

        migrationBuilder.DropTable(
            "wiki-chat-dialog");

        migrationBuilder.DropTable(
            "wiki-chat-dialog-history");

        migrationBuilder.DropTable(
            "wiki-chat-share");

        migrationBuilder.DropTable(
            "wiki-file-storages");

        migrationBuilder.DropTable(
            "wiki-function-calls");

        migrationBuilder.DropTable(
            "wiki-users");

        migrationBuilder.DropTable(
            "wiki-wiki-details");

        migrationBuilder.DropTable(
            "wiki-wikis");
    }
}