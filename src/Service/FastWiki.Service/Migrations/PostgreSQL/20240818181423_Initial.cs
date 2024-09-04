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
                    Name = table.Column<string>(type: "text", nullable: true),
                    Prompt = table.Column<string>(type: "text", nullable: true),
                    ChatModel = table.Column<string>(type: "text", nullable: true),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    MaxResponseToken = table.Column<int>(type: "integer", nullable: false),
                    Template = table.Column<string>(type: "text", nullable: true),
                    Parameter = table.Column<string>(type: "text", nullable: true),
                    Opener = table.Column<string>(type: "text", nullable: true),
                    WikiIds = table.Column<string>(type: "text", nullable: true),
                    ReferenceUpperLimit = table.Column<int>(type: "integer", nullable: false),
                    Relevancy = table.Column<double>(type: "double precision", nullable: false),
                    NoReplyFoundTemplate = table.Column<string>(type: "text", nullable: true),
                    ShowSourceFile = table.Column<bool>(type: "boolean", nullable: false),
                    Extend = table.Column<string>(type: "text", nullable: true),
                    FunctionIds = table.Column<string>(type: "text", nullable: true),
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
                name: "wiki-chat-record",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<string>(type: "text", nullable: true),
                    Question = table.Column<string>(type: "text", nullable: true),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-record", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-chat-share",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ChatApplicationId = table.Column<string>(type: "text", nullable: true),
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
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Path = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    IsCompression = table.Column<bool>(type: "boolean", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
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
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Parameters = table.Column<string>(type: "text", nullable: true),
                    Items = table.Column<string>(type: "text", nullable: true),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    Imports = table.Column<string>(type: "text", nullable: true),
                    Main = table.Column<string>(type: "text", nullable: true),
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
                name: "wiki-histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemoryId = table.Column<string>(type: "text", nullable: false),
                    PrevValue = table.Column<string>(type: "text", nullable: false),
                    NewValue = table.Column<string>(type: "text", nullable: false),
                    Event = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    TrackId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-quantized-lists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WikiId = table.Column<long>(type: "bigint", nullable: false),
                    WikiDetailId = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    ProcessTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-quantized-lists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-questions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<string>(type: "text", nullable: true),
                    Question = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Account = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
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
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Path = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FileId = table.Column<long>(type: "bigint", nullable: false),
                    DataCount = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    State = table.Column<int>(type: "integer", nullable: false),
                    MaxTokensPerParagraph = table.Column<int>(type: "integer", nullable: false),
                    MaxTokensPerLine = table.Column<int>(type: "integer", nullable: false),
                    OverlappingTokens = table.Column<int>(type: "integer", nullable: false),
                    Mode = table.Column<int>(type: "integer", nullable: false),
                    TrainingPattern = table.Column<int>(type: "integer", nullable: false),
                    QAPromptTemplate = table.Column<string>(type: "text", nullable: true),
                    Creator = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<long>(type: "bigint", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    EmbeddingModel = table.Column<string>(type: "text", nullable: true),
                    VectorType = table.Column<int>(type: "integer", nullable: false),
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
                values: new object[] { new Guid("cd7b65ff-6d9c-4f78-930e-58cb452f1395"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 8, 18, 18, 14, 22, 847, DateTimeKind.Utc).AddTicks(2836), null, "239573049@qq.com", false, false, new DateTime(2024, 8, 18, 18, 14, 22, 847, DateTimeKind.Utc).AddTicks(2838), null, "admin", "805f068fde592b9798123d686ce56984", "13049809673", 2, "48e878a34df94d04bc9c8033ddc5f8bb" });

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-application_Name",
                table: "wiki-chat-application",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-record_CreationTime",
                table: "wiki-chat-record",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-share_ChatApplicationId",
                table: "wiki-chat-share",
                column: "ChatApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-function-calls_CreationTime",
                table: "wiki-function-calls",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-histories_MemoryId",
                table: "wiki-histories",
                column: "MemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-histories_TrackId",
                table: "wiki-histories",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-histories_UserId",
                table: "wiki-histories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-quantized-lists_WikiDetailId",
                table: "wiki-quantized-lists",
                column: "WikiDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-quantized-lists_WikiId",
                table: "wiki-quantized-lists",
                column: "WikiId");

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
                name: "wiki-chat-record");

            migrationBuilder.DropTable(
                name: "wiki-chat-share");

            migrationBuilder.DropTable(
                name: "wiki-file-storages");

            migrationBuilder.DropTable(
                name: "wiki-function-calls");

            migrationBuilder.DropTable(
                name: "wiki-histories");

            migrationBuilder.DropTable(
                name: "wiki-quantized-lists");

            migrationBuilder.DropTable(
                name: "wiki-questions");

            migrationBuilder.DropTable(
                name: "wiki-users");

            migrationBuilder.DropTable(
                name: "wiki-wiki-details");

            migrationBuilder.DropTable(
                name: "wiki-wikis");
        }
    }
}
