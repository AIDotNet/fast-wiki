using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationForFunctionCall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wiki-chat-application-for-function-call");

            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "0d0d82112fd34726b5b4dba0aad5ac56");

            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "0f6df89aec924c25ad1a258c422930d9");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("7b3c7ef1-eef1-4b75-a86c-5d57ab00f041"));

            migrationBuilder.AddColumn<string>(
                name: "FunctionIds",
                table: "wiki-chat-application",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "wiki-fast-models",
                columns: new[] { "Id", "ApiKey", "CreationTime", "Creator", "Description", "Enable", "IsDeleted", "Models", "ModificationTime", "Modifier", "Name", "Order", "TestTime", "Type", "Url", "UsedQuota" },
                values: new object[,]
                {
                    { "d114f3219d8c4398be2b66af4edbe85e", "", new DateTime(2024, 4, 5, 10, 24, 24, 310, DateTimeKind.Utc).AddTicks(6618), null, "星火大模型", true, false, "[\"SparkDesk-v3.5\",\"SparkDesk-v3.1\",\"SparkDesk-v1.5\",\"SparkDesk-v2.1\"]", new DateTime(2024, 4, 5, 10, 24, 24, 310, DateTimeKind.Utc).AddTicks(6619), null, "SparkDesk", 1, null, "SparkDesk", "", 0L },
                    { "ee2e461ab124432485c4688ca569e174", "", new DateTime(2024, 4, 5, 10, 24, 24, 310, DateTimeKind.Utc).AddTicks(6603), null, "OpenAI", true, false, "[\"gpt-3.5-turbo\",\"gpt-3.5-turbo-0125\",\"gpt-3.5-turbo-1106\",\"gpt-3.5-turbo-16k\",\"gpt-3.5-turbo-0613\",\"gpt-3.5-turbo-16k-0613\",\"gpt-4-0125-preview\",\"gpt-4-turbo-preview\",\"gpt-4-1106-preview\",\"gpt-4-vision-preview\",\"gpt-4-1106-vision-preview\",\"gpt-4\",\"gpt-4-0613\",\"gpt-4-32k\",\"gpt-4-32k-0613\"]", new DateTime(2024, 4, 5, 10, 24, 24, 310, DateTimeKind.Utc).AddTicks(6604), null, "OpenAI", 1, null, "OpenAI", "https://api.openai.com/", 0L }
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("c2e43fc3-d870-48ea-a7e2-62586ee6b7ca"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 4, 5, 10, 24, 24, 310, DateTimeKind.Utc).AddTicks(5260), null, "239573049@qq.com", false, false, new DateTime(2024, 4, 5, 10, 24, 24, 310, DateTimeKind.Utc).AddTicks(5262), null, "admin", "949de09c9cac12c3f5ceb79f9c49fe3b", "13049809673", 2, "524fcf01d6bd4b1fb9289d3e41ec0044" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "d114f3219d8c4398be2b66af4edbe85e");

            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "ee2e461ab124432485c4688ca569e174");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("c2e43fc3-d870-48ea-a7e2-62586ee6b7ca"));

            migrationBuilder.DropColumn(
                name: "FunctionIds",
                table: "wiki-chat-application");

            migrationBuilder.CreateTable(
                name: "wiki-chat-application-for-function-call",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatApplicationId = table.Column<string>(type: "text", nullable: false),
                    FunctionCallId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-application-for-function-call", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "wiki-fast-models",
                columns: new[] { "Id", "ApiKey", "CreationTime", "Creator", "Description", "Enable", "IsDeleted", "Models", "ModificationTime", "Modifier", "Name", "Order", "TestTime", "Type", "Url", "UsedQuota" },
                values: new object[,]
                {
                    { "0d0d82112fd34726b5b4dba0aad5ac56", "", new DateTime(2024, 4, 5, 10, 0, 41, 182, DateTimeKind.Utc).AddTicks(9398), null, "OpenAI", true, false, "[\"gpt-3.5-turbo\",\"gpt-3.5-turbo-0125\",\"gpt-3.5-turbo-1106\",\"gpt-3.5-turbo-16k\",\"gpt-3.5-turbo-0613\",\"gpt-3.5-turbo-16k-0613\",\"gpt-4-0125-preview\",\"gpt-4-turbo-preview\",\"gpt-4-1106-preview\",\"gpt-4-vision-preview\",\"gpt-4-1106-vision-preview\",\"gpt-4\",\"gpt-4-0613\",\"gpt-4-32k\",\"gpt-4-32k-0613\"]", new DateTime(2024, 4, 5, 10, 0, 41, 182, DateTimeKind.Utc).AddTicks(9399), null, "OpenAI", 1, null, "OpenAI", "https://api.openai.com/", 0L },
                    { "0f6df89aec924c25ad1a258c422930d9", "", new DateTime(2024, 4, 5, 10, 0, 41, 182, DateTimeKind.Utc).AddTicks(9441), null, "星火大模型", true, false, "[\"SparkDesk-v3.5\",\"SparkDesk-v3.1\",\"SparkDesk-v1.5\",\"SparkDesk-v2.1\"]", new DateTime(2024, 4, 5, 10, 0, 41, 182, DateTimeKind.Utc).AddTicks(9442), null, "SparkDesk", 1, null, "SparkDesk", "", 0L }
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("7b3c7ef1-eef1-4b75-a86c-5d57ab00f041"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 4, 5, 10, 0, 41, 182, DateTimeKind.Utc).AddTicks(8122), null, "239573049@qq.com", false, false, new DateTime(2024, 4, 5, 10, 0, 41, 182, DateTimeKind.Utc).AddTicks(8124), null, "admin", "d709a005a9b59b7265d7de556d7bbf83", "13049809673", 2, "f557e936db0a44eb870f14a24dbc7723" });

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-application-for-function-call_ChatApplicationId",
                table: "wiki-chat-application-for-function-call",
                column: "ChatApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-application-for-function-call_FunctionCallId",
                table: "wiki-chat-application-for-function-call",
                column: "FunctionCallId");
        }
    }
}
