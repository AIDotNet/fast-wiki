using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class Function : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "adf1fe462e7a4f7a93ee6951ca31d63c");

            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "e124eb014a944dd79f3e4823461086d2");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("f0b2b97a-1f5f-4741-8110-7dc382b52025"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "wiki-fast-models",
                columns: new[] { "Id", "ApiKey", "CreationTime", "Creator", "Description", "Enable", "IsDeleted", "Models", "ModificationTime", "Modifier", "Name", "Order", "TestTime", "Type", "Url", "UsedQuota" },
                values: new object[,]
                {
                    { "adf1fe462e7a4f7a93ee6951ca31d63c", "", new DateTime(2024, 3, 24, 14, 44, 52, 662, DateTimeKind.Utc).AddTicks(7163), null, "OpenAI", true, false, "[\"gpt-3.5-turbo\",\"gpt-3.5-turbo-0125\",\"gpt-3.5-turbo-1106\",\"gpt-3.5-turbo-16k\",\"gpt-3.5-turbo-0613\",\"gpt-3.5-turbo-16k-0613\",\"gpt-4-0125-preview\",\"gpt-4-turbo-preview\",\"gpt-4-1106-preview\",\"gpt-4-vision-preview\",\"gpt-4-1106-vision-preview\",\"gpt-4\",\"gpt-4-0613\",\"gpt-4-32k\",\"gpt-4-32k-0613\"]", new DateTime(2024, 3, 24, 14, 44, 52, 662, DateTimeKind.Utc).AddTicks(7165), null, "OpenAI", 1, null, "OpenAI", "https://api.openai.com/", 0L },
                    { "e124eb014a944dd79f3e4823461086d2", "", new DateTime(2024, 3, 24, 14, 44, 52, 662, DateTimeKind.Utc).AddTicks(7196), null, "星火大模型", true, false, "[\"SparkDesk-v3.5\",\"SparkDesk-v3.1\",\"SparkDesk-v1.5\",\"SparkDesk-v2.1\"]", new DateTime(2024, 3, 24, 14, 44, 52, 662, DateTimeKind.Utc).AddTicks(7197), null, "SparkDesk", 1, null, "SparkDesk", "", 0L }
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("f0b2b97a-1f5f-4741-8110-7dc382b52025"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 24, 14, 44, 52, 662, DateTimeKind.Utc).AddTicks(5388), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 24, 14, 44, 52, 662, DateTimeKind.Utc).AddTicks(5390), null, "admin", "2a2e3a0f18f38c8ba99bee8e499ed572", "13049809673", 2, "124dc3dc26674fad9ea6adea9eb231b6" });
        }
    }
}
