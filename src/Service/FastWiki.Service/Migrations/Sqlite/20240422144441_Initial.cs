using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations.Sqlite
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
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Prompt = table.Column<string>(type: "TEXT", nullable: false),
                    ChatModel = table.Column<string>(type: "TEXT", nullable: false),
                    Temperature = table.Column<double>(type: "REAL", nullable: false),
                    MaxResponseToken = table.Column<int>(type: "INTEGER", nullable: false),
                    Template = table.Column<string>(type: "TEXT", nullable: false),
                    Parameter = table.Column<string>(type: "TEXT", nullable: false),
                    Opener = table.Column<string>(type: "TEXT", nullable: false),
                    WikiIds = table.Column<string>(type: "TEXT", nullable: false),
                    ReferenceUpperLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    Relevancy = table.Column<double>(type: "REAL", nullable: false),
                    NoReplyFoundTemplate = table.Column<string>(type: "TEXT", nullable: true),
                    ShowSourceFile = table.Column<bool>(type: "INTEGER", nullable: false),
                    Extend = table.Column<string>(type: "TEXT", nullable: false),
                    FunctionIds = table.Column<string>(type: "TEXT", nullable: false),
                    Creator = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modifier = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-chat-dialog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ChatId = table.Column<string>(type: "TEXT", nullable: true),
                    ApplicationId = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Creator = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modifier = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-dialog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-chat-dialog-history",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ChatDialogId = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: -1, nullable: false),
                    TokenConsumption = table.Column<int>(type: "INTEGER", nullable: false),
                    Current = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    ReferenceFile = table.Column<string>(type: "TEXT", nullable: false),
                    Creator = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modifier = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-dialog-history", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-chat-share",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ChatApplicationId = table.Column<string>(type: "TEXT", nullable: false),
                    Expires = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UsedToken = table.Column<long>(type: "INTEGER", nullable: false),
                    AvailableToken = table.Column<long>(type: "INTEGER", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    APIKey = table.Column<string>(type: "TEXT", nullable: true),
                    Creator = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modifier = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-share", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-file-storages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Path = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    IsCompression = table.Column<bool>(type: "INTEGER", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Creator = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modifier = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-file-storages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-function-calls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Parameters = table.Column<string>(type: "TEXT", nullable: false),
                    Items = table.Column<string>(type: "TEXT", nullable: false),
                    Enable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Imports = table.Column<string>(type: "TEXT", nullable: false),
                    Main = table.Column<string>(type: "TEXT", nullable: false),
                    Creator = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modifier = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-function-calls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Account = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Salt = table.Column<string>(type: "TEXT", nullable: false),
                    Avatar = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    IsDisable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    Creator = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modifier = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-wiki-details",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WikiId = table.Column<long>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Path = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    FileId = table.Column<long>(type: "INTEGER", nullable: false),
                    DataCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Creator = table.Column<long>(type: "INTEGER", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modifier = table.Column<long>(type: "INTEGER", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MaxTokensPerParagraph = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxTokensPerLine = table.Column<int>(type: "INTEGER", nullable: false),
                    OverlappingTokens = table.Column<int>(type: "INTEGER", nullable: false),
                    Mode = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingPattern = table.Column<int>(type: "INTEGER", nullable: false),
                    QAPromptTemplate = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-wiki-details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-wikis",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    EmbeddingModel = table.Column<string>(type: "TEXT", nullable: false),
                    Creator = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modifier = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-wikis", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("c8b3b44a-6e7c-4f11-88b2-ed846e953b7c"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 4, 22, 14, 44, 40, 964, DateTimeKind.Utc).AddTicks(8730), null, "239573049@qq.com", false, false, new DateTime(2024, 4, 22, 14, 44, 40, 964, DateTimeKind.Utc).AddTicks(8733), null, "admin", "418817cad9f0eae28501500c5a6e31e9", "13049809673", 2, "06a074ae10a44f2ea7b0de027562723e" });

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
