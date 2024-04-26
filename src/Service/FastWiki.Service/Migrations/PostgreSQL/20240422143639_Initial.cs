using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FastWiki.Service.Migrations.PostgreSQL
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wiki-chat-application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Prompt = table.Column<string>(type: "text", nullable: false),
                    ChatModel = table.Column<string>(type: "text", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    MaxResponseToken = table.Column<int>(type: "integer", nullable: false),
                    Template = table.Column<string>(type: "text", nullable: false),
                    Parameter = table.Column<string>(type: "text", nullable: false),
                    Opener = table.Column<string>(type: "text", nullable: false),
                    WikiIds = table.Column<string>(type: "text", nullable: false),
                    ReferenceUpperLimit = table.Column<int>(type: "integer", nullable: false),
                    Relevancy = table.Column<double>(type: "double precision", nullable: false),
                    NoReplyFoundTemplate = table.Column<string>(type: "text", nullable: true),
                    ShowSourceFile = table.Column<bool>(type: "boolean", nullable: false),
                    Extend = table.Column<string>(type: "text", nullable: false),
                    FunctionIds = table.Column<string>(type: "text", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-chat-dialog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ChatId = table.Column<string>(type: "text", nullable: true),
                    ApplicationId = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-dialog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-chat-dialog-history",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ChatDialogId = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", maxLength: -1, nullable: false),
                    TokenConsumption = table.Column<int>(type: "integer", nullable: false),
                    Current = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ReferenceFile = table.Column<string>(type: "text", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-dialog-history", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-chat-share",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ChatApplicationId = table.Column<string>(type: "text", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UsedToken = table.Column<long>(type: "bigint", nullable: false),
                    AvailableToken = table.Column<long>(type: "bigint", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "integer", nullable: false),
                    APIKey = table.Column<string>(type: "text", nullable: true),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-share", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-file-storages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Path = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    IsCompression = table.Column<bool>(type: "boolean", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-file-storages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-function-calls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Parameters = table.Column<string>(type: "text", nullable: false),
                    Items = table.Column<string>(type: "text", nullable: false),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    Imports = table.Column<string>(type: "text", nullable: false),
                    Main = table.Column<string>(type: "text", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-function-calls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Account = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    IsDisable = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-wiki-details",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WikiId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Path = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FileId = table.Column<long>(type: "bigint", nullable: false),
                    DataCount = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Creator = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<long>(type: "bigint", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MaxTokensPerParagraph = table.Column<int>(type: "integer", nullable: false),
                    MaxTokensPerLine = table.Column<int>(type: "integer", nullable: false),
                    OverlappingTokens = table.Column<int>(type: "integer", nullable: false),
                    Mode = table.Column<int>(type: "integer", nullable: false),
                    TrainingPattern = table.Column<int>(type: "integer", nullable: false),
                    QAPromptTemplate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-wiki-details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-wikis",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    EmbeddingModel = table.Column<string>(type: "text", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-wikis", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("81ba41a1-27df-4f6c-b327-dfec1bce8c85"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 4, 22, 14, 36, 39, 431, DateTimeKind.Utc).AddTicks(7635), null, "239573049@qq.com", false, false, new DateTime(2024, 4, 22, 14, 36, 39, 431, DateTimeKind.Utc).AddTicks(7638), null, "admin", "ae322a2c4c237df3b8683703e51442aa", "13049809673", 2, "19ea5ff625024a1d80bbf86c054e132c" });

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-application_Name",
                table: "wiki-chat-application",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-dialog_ChatId",
                table: "wiki-chat-dialog",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-dialog-history_ChatDialogId",
                table: "wiki-chat-dialog-history",
                column: "ChatDialogId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-dialog-history_Creator",
                table: "wiki-chat-dialog-history",
                column: "Creator");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-share_ChatApplicationId",
                table: "wiki-chat-share",
                column: "ChatApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-function-calls_CreationTime",
                table: "wiki-function-calls",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-wikis_Name",
                table: "wiki-wikis",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wiki-chat-application");

            migrationBuilder.DropTable(
                name: "wiki-chat-dialog");

            migrationBuilder.DropTable(
                name: "wiki-chat-dialog-history");

            migrationBuilder.DropTable(
                name: "wiki-chat-share");

            migrationBuilder.DropTable(
                name: "wiki-file-storages");

            migrationBuilder.DropTable(
                name: "wiki-function-calls");

            migrationBuilder.DropTable(
                name: "wiki-users");

            migrationBuilder.DropTable(
                name: "wiki-wiki-details");

            migrationBuilder.DropTable(
                name: "wiki-wikis");
        }
    }
}
