using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddFastModel_Logger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("a899e701-4d58-4809-843f-a024e3bf629c"));

            migrationBuilder.CreateTable(
                name: "wiki-fast-models",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ApiKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Models = table.Column<string>(type: "text", maxLength: -1, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    TestTime = table.Column<long>(type: "bigint", nullable: true),
                    UsedQuota = table.Column<long>(type: "bigint", nullable: false),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-fast-models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-model-logger",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FastModelId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApplicationId = table.Column<string>(type: "text", nullable: true),
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    PromptCount = table.Column<int>(type: "integer", nullable: false),
                    ComplementCount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-model-logger", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("1bca921f-0f35-42e5-8cef-c530e36c8f54"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 20, 17, 20, 45, 551, DateTimeKind.Utc).AddTicks(5537), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 20, 17, 20, 45, 551, DateTimeKind.Utc).AddTicks(5538), null, "admin", "22cf0ff8fc2d1a8c008cbbdad311ed4a", "13049809673", 2, "19520e9012974aa7ad93a908f373a81e" });

            migrationBuilder.CreateIndex(
                name: "IX_wiki-fast-models_Name",
                table: "wiki-fast-models",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-fast-models_Type",
                table: "wiki-fast-models",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-model-logger_ApiKey",
                table: "wiki-model-logger",
                column: "ApiKey");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-model-logger_ApplicationId",
                table: "wiki-model-logger",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-model-logger_CreationTime",
                table: "wiki-model-logger",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-model-logger_FastModelId",
                table: "wiki-model-logger",
                column: "FastModelId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-model-logger_Type",
                table: "wiki-model-logger",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-model-logger_UserId",
                table: "wiki-model-logger",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wiki-fast-models");

            migrationBuilder.DropTable(
                name: "wiki-model-logger");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("1bca921f-0f35-42e5-8cef-c530e36c8f54"));

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("a899e701-4d58-4809-843f-a024e3bf629c"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 19, 17, 57, 2, 871, DateTimeKind.Utc).AddTicks(3601), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 19, 17, 57, 2, 871, DateTimeKind.Utc).AddTicks(3603), null, "admin", "2f204309fb0afa4c2bdba7b2904ea10e", "13049809673", 2, "ab3b086ec59a45cfbf65398ad8f64fdb" });
        }
    }
}
