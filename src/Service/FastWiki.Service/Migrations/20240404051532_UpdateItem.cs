using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "2cd8b494cda54acdb5842ad3618ad4b8");

            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "d13894bebf9649059a35886014fcd174");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("2bbdaf99-5de9-477e-ac04-5200102bc1b6"));

            migrationBuilder.CreateTable(
                name: "wiki-chat-application-for-function-call",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FunctionCallId = table.Column<long>(type: "bigint", nullable: false),
                    ChatApplicationId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-chat-application-for-function-call", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wiki-function-calls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Cotnent = table.Column<string>(type: "text", nullable: false),
                    Parameters = table.Column<string>(type: "text", nullable: false),
                    Items = table.Column<string>(type: "text", nullable: false),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    Imports = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.InsertData(
                table: "wiki-fast-models",
                columns: new[] { "Id", "ApiKey", "CreationTime", "Creator", "Description", "Enable", "IsDeleted", "Models", "ModificationTime", "Modifier", "Name", "Order", "TestTime", "Type", "Url", "UsedQuota" },
                values: new object[,]
                {
                    { "a638f34b0a36421ea728d18ab4b5cd09", "", new DateTime(2024, 4, 4, 5, 15, 31, 800, DateTimeKind.Utc).AddTicks(6310), null, "OpenAI", true, false, "[\"gpt-3.5-turbo\",\"gpt-3.5-turbo-0125\",\"gpt-3.5-turbo-1106\",\"gpt-3.5-turbo-16k\",\"gpt-3.5-turbo-0613\",\"gpt-3.5-turbo-16k-0613\",\"gpt-4-0125-preview\",\"gpt-4-turbo-preview\",\"gpt-4-1106-preview\",\"gpt-4-vision-preview\",\"gpt-4-1106-vision-preview\",\"gpt-4\",\"gpt-4-0613\",\"gpt-4-32k\",\"gpt-4-32k-0613\"]", new DateTime(2024, 4, 4, 5, 15, 31, 800, DateTimeKind.Utc).AddTicks(6311), null, "OpenAI", 1, null, "OpenAI", "https://api.openai.com/", 0L },
                    { "ea32c1ad5ad74867a820885cc6a5531f", "", new DateTime(2024, 4, 4, 5, 15, 31, 800, DateTimeKind.Utc).AddTicks(6327), null, "星火大模型", true, false, "[\"SparkDesk-v3.5\",\"SparkDesk-v3.1\",\"SparkDesk-v1.5\",\"SparkDesk-v2.1\"]", new DateTime(2024, 4, 4, 5, 15, 31, 800, DateTimeKind.Utc).AddTicks(6327), null, "SparkDesk", 1, null, "SparkDesk", "", 0L }
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("ecbf47bb-f623-47bc-8e81-3781a685ccd1"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 4, 4, 5, 15, 31, 800, DateTimeKind.Utc).AddTicks(5008), null, "239573049@qq.com", false, false, new DateTime(2024, 4, 4, 5, 15, 31, 800, DateTimeKind.Utc).AddTicks(5011), null, "admin", "5e5be0250ed7fbb73c06d596a52969fa", "13049809673", 2, "a48ddf2a3e414b748b69697fbc6e6c29" });

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-application-for-function-call_ChatApplicationId",
                table: "wiki-chat-application-for-function-call",
                column: "ChatApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-chat-application-for-function-call_FunctionCallId",
                table: "wiki-chat-application-for-function-call",
                column: "FunctionCallId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-function-calls_CreationTime",
                table: "wiki-function-calls",
                column: "CreationTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wiki-chat-application-for-function-call");

            migrationBuilder.DropTable(
                name: "wiki-function-calls");

            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "a638f34b0a36421ea728d18ab4b5cd09");

            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "ea32c1ad5ad74867a820885cc6a5531f");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("ecbf47bb-f623-47bc-8e81-3781a685ccd1"));

            migrationBuilder.InsertData(
                table: "wiki-fast-models",
                columns: new[] { "Id", "ApiKey", "CreationTime", "Creator", "Description", "Enable", "IsDeleted", "Models", "ModificationTime", "Modifier", "Name", "Order", "TestTime", "Type", "Url", "UsedQuota" },
                values: new object[,]
                {
                    { "2cd8b494cda54acdb5842ad3618ad4b8", "", new DateTime(2024, 4, 4, 4, 59, 43, 176, DateTimeKind.Utc).AddTicks(6756), null, "星火大模型", true, false, "[\"SparkDesk-v3.5\",\"SparkDesk-v3.1\",\"SparkDesk-v1.5\",\"SparkDesk-v2.1\"]", new DateTime(2024, 4, 4, 4, 59, 43, 176, DateTimeKind.Utc).AddTicks(6756), null, "SparkDesk", 1, null, "SparkDesk", "", 0L },
                    { "d13894bebf9649059a35886014fcd174", "", new DateTime(2024, 4, 4, 4, 59, 43, 176, DateTimeKind.Utc).AddTicks(6737), null, "OpenAI", true, false, "[\"gpt-3.5-turbo\",\"gpt-3.5-turbo-0125\",\"gpt-3.5-turbo-1106\",\"gpt-3.5-turbo-16k\",\"gpt-3.5-turbo-0613\",\"gpt-3.5-turbo-16k-0613\",\"gpt-4-0125-preview\",\"gpt-4-turbo-preview\",\"gpt-4-1106-preview\",\"gpt-4-vision-preview\",\"gpt-4-1106-vision-preview\",\"gpt-4\",\"gpt-4-0613\",\"gpt-4-32k\",\"gpt-4-32k-0613\"]", new DateTime(2024, 4, 4, 4, 59, 43, 176, DateTimeKind.Utc).AddTicks(6738), null, "OpenAI", 1, null, "OpenAI", "https://api.openai.com/", 0L }
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("2bbdaf99-5de9-477e-ac04-5200102bc1b6"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 4, 4, 4, 59, 43, 176, DateTimeKind.Utc).AddTicks(5516), null, "239573049@qq.com", false, false, new DateTime(2024, 4, 4, 4, 59, 43, 176, DateTimeKind.Utc).AddTicks(5518), null, "admin", "b2bcd36ed6273a810b85db46613496cb", "13049809673", 2, "04cfc2e784f94e5d82d1fa45efac7b7d" });
        }
    }
}
