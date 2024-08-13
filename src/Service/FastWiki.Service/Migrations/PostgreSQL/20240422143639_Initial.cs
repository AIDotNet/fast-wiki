using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FastWiki.Service.Migrations.PostgreSQL;

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
                Id = table.Column<string>("text", nullable: false),
                Name = table.Column<string>("text", nullable: false),
                Prompt = table.Column<string>("text", nullable: false),
                ChatModel = table.Column<string>("text", nullable: false),
                Temperature = table.Column<double>("double precision", nullable: false),
                MaxResponseToken = table.Column<int>("integer", nullable: false),
                Template = table.Column<string>("text", nullable: false),
                Parameter = table.Column<string>("text", nullable: false),
                Opener = table.Column<string>("text", nullable: false),
                WikiIds = table.Column<string>("text", nullable: false),
                ReferenceUpperLimit = table.Column<int>("integer", nullable: false),
                Relevancy = table.Column<double>("double precision", nullable: false),
                NoReplyFoundTemplate = table.Column<string>("text", nullable: true),
                ShowSourceFile = table.Column<bool>("boolean", nullable: false),
                Extend = table.Column<string>("text", nullable: false),
                FunctionIds = table.Column<string>("text", nullable: false),
                Creator = table.Column<Guid>("uuid", nullable: true),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: true),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-application", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-chat-dialog",
            table => new
            {
                Id = table.Column<string>("text", nullable: false),
                Name = table.Column<string>("text", nullable: false),
                ChatId = table.Column<string>("text", nullable: true),
                ApplicationId = table.Column<string>("text", nullable: false),
                Description = table.Column<string>("text", nullable: false),
                Type = table.Column<int>("integer", nullable: false),
                Creator = table.Column<Guid>("uuid", nullable: false),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: false),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-dialog", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-chat-dialog-history",
            table => new
            {
                Id = table.Column<string>("text", nullable: false),
                ChatDialogId = table.Column<string>("text", nullable: false),
                Content = table.Column<string>("text", maxLength: -1, nullable: false),
                TokenConsumption = table.Column<int>("integer", nullable: false),
                Current = table.Column<bool>("boolean", nullable: false),
                Type = table.Column<int>("integer", nullable: false),
                ReferenceFile = table.Column<string>("text", nullable: false),
                Creator = table.Column<Guid>("uuid", nullable: false),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: false),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-dialog-history", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-chat-share",
            table => new
            {
                Id = table.Column<string>("text", nullable: false),
                Name = table.Column<string>("text", nullable: false),
                ChatApplicationId = table.Column<string>("text", nullable: false),
                Expires = table.Column<DateTime>("timestamp without time zone", nullable: true),
                UsedToken = table.Column<long>("bigint", nullable: false),
                AvailableToken = table.Column<long>("bigint", nullable: false),
                AvailableQuantity = table.Column<int>("integer", nullable: false),
                APIKey = table.Column<string>("text", nullable: true),
                Creator = table.Column<Guid>("uuid", nullable: false),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: false),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-share", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-file-storages",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                Path = table.Column<string>("character varying(200)", maxLength: 200, nullable: false),
                Size = table.Column<long>("bigint", nullable: false),
                IsCompression = table.Column<bool>("boolean", nullable: false),
                FullName = table.Column<string>("text", nullable: false),
                Creator = table.Column<Guid>("uuid", nullable: true),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: true),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-file-storages", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-function-calls",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>("text", nullable: false),
                Description = table.Column<string>("text", nullable: false),
                Content = table.Column<string>("text", nullable: false),
                Parameters = table.Column<string>("text", nullable: false),
                Items = table.Column<string>("text", nullable: false),
                Enable = table.Column<bool>("boolean", nullable: false),
                Imports = table.Column<string>("text", nullable: false),
                Main = table.Column<string>("text", nullable: false),
                Creator = table.Column<Guid>("uuid", nullable: false),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: false),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-function-calls", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-users",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Account = table.Column<string>("text", nullable: false),
                Name = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                Password = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                Salt = table.Column<string>("text", nullable: false),
                Avatar = table.Column<string>("text", nullable: false),
                Email = table.Column<string>("text", nullable: false),
                Phone = table.Column<string>("text", nullable: false),
                IsDisable = table.Column<bool>("boolean", nullable: false),
                Role = table.Column<int>("integer", nullable: false),
                Creator = table.Column<Guid>("uuid", nullable: true),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: true),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-users", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-wiki-details",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                WikiId = table.Column<long>("bigint", nullable: false),
                FileName = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                Path = table.Column<string>("character varying(200)", maxLength: 200, nullable: false),
                FileId = table.Column<long>("bigint", nullable: false),
                DataCount = table.Column<int>("integer", nullable: false),
                Type = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                State = table.Column<int>("integer", nullable: false),
                Creator = table.Column<long>("bigint", nullable: false),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<long>("bigint", nullable: false),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                MaxTokensPerParagraph = table.Column<int>("integer", nullable: false),
                MaxTokensPerLine = table.Column<int>("integer", nullable: false),
                OverlappingTokens = table.Column<int>("integer", nullable: false),
                Mode = table.Column<int>("integer", nullable: false),
                TrainingPattern = table.Column<int>("integer", nullable: false),
                QAPromptTemplate = table.Column<string>("text", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-wiki-details", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-wikis",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Icon = table.Column<string>("text", nullable: false),
                Name = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                Model = table.Column<string>("text", nullable: false),
                EmbeddingModel = table.Column<string>("text", nullable: false),
                Creator = table.Column<Guid>("uuid", nullable: true),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: true),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false)
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
                new Guid("81ba41a1-27df-4f6c-b327-dfec1bce8c85"), "admin",
                "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                new DateTime(2024, 4, 22, 14, 36, 39, 431, DateTimeKind.Utc).AddTicks(7635), null, "239573049@qq.com",
                false, false, new DateTime(2024, 4, 22, 14, 36, 39, 431, DateTimeKind.Utc).AddTicks(7638), null,
                "admin", "ae322a2c4c237df3b8683703e51442aa", "13049809673", 2, "19ea5ff625024a1d80bbf86c054e132c"
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