using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastWiki.Service.Migrations.Master
{
    /// <inheritdoc />
    public partial class RemoveModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wiki-fast-models");

            migrationBuilder.DropTable(
                name: "wiki-model-logger");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("3a2370db-a80f-4bb4-86ba-37c71759c3c5"));

            migrationBuilder.DropColumn(
                name: "ChatType",
                table: "wiki-chat-application");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("a8c91b9f-153e-43ba-b9c3-9f2c8c167cd7"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 4, 22, 14, 1, 8, 508, DateTimeKind.Utc).AddTicks(7230), null, "239573049@qq.com", false, false, new DateTime(2024, 4, 22, 14, 1, 8, 508, DateTimeKind.Utc).AddTicks(7232), null, "admin", "e0480b9e1d44661da57adaee98946341", "13049809673", 2, "31b31607b45a48689f3a43277c1f8a36" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("a8c91b9f-153e-43ba-b9c3-9f2c8c167cd7"));

            migrationBuilder.AddColumn<string>(
                name: "ChatType",
                table: "wiki-chat-application",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "wiki-fast-models",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Models = table.Column<string>(type: "text", maxLength: -1, nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    TestTime = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    UsedQuota = table.Column<long>(type: "bigint", nullable: false)
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
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<string>(type: "text", nullable: true),
                    ComplementCount = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FastModelId = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    PromptCount = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-model-logger", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "wiki-fast-models",
                columns: new[] { "Id", "ApiKey", "CreationTime", "Creator", "Description", "Enable", "IsDeleted", "Models", "ModificationTime", "Modifier", "Name", "Order", "TestTime", "Type", "Url", "UsedQuota" },
                values: new object[,]
                {
                    { "0b01f0fddb63483e8174fba06522108a", "", new DateTime(2024, 4, 16, 5, 19, 56, 870, DateTimeKind.Utc).AddTicks(5326), null, "星火大模型", true, false, "[\"SparkDesk-v3.5\",\"SparkDesk-v3.1\",\"SparkDesk-v1.5\",\"SparkDesk-v2.1\"]", new DateTime(2024, 4, 16, 5, 19, 56, 870, DateTimeKind.Utc).AddTicks(5326), null, "SparkDesk", 1, null, "SparkDesk", "", 0L },
                    { "d28cb989669c46fba386666e84a2c72e", "", new DateTime(2024, 4, 16, 5, 19, 56, 870, DateTimeKind.Utc).AddTicks(5311), null, "OpenAI", true, false, "[\"gpt-3.5-turbo\",\"gpt-3.5-turbo-0125\",\"gpt-3.5-turbo-1106\",\"gpt-3.5-turbo-16k\",\"gpt-3.5-turbo-0613\",\"gpt-3.5-turbo-16k-0613\",\"gpt-4-0125-preview\",\"gpt-4-turbo-preview\",\"gpt-4-1106-preview\",\"gpt-4-vision-preview\",\"gpt-4-1106-vision-preview\",\"gpt-4\",\"gpt-4-0613\",\"gpt-4-32k\",\"gpt-4-32k-0613\"]", new DateTime(2024, 4, 16, 5, 19, 56, 870, DateTimeKind.Utc).AddTicks(5312), null, "OpenAI", 1, null, "OpenAI", "http://ai-api.token-ai.cn/", 0L }
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("3a2370db-a80f-4bb4-86ba-37c71759c3c5"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 4, 16, 5, 19, 56, 870, DateTimeKind.Utc).AddTicks(3675), null, "239573049@qq.com", false, false, new DateTime(2024, 4, 16, 5, 19, 56, 870, DateTimeKind.Utc).AddTicks(3678), null, "admin", "58de2614378ee2067350a70bb789abd5", "13049809673", 2, "98d6415225ce4820a571359bd5192b91" });

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
    }
}
