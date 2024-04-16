using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDetaiis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MaxTokensPerLine",
                table: "wiki-wiki-details",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxTokensPerParagraph",
                table: "wiki-wiki-details",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "wiki-wiki-details",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverlappingTokens",
                table: "wiki-wiki-details",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QAPromptTemplate",
                table: "wiki-wiki-details",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingPattern",
                table: "wiki-wiki-details",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "0b01f0fddb63483e8174fba06522108a");

            migrationBuilder.DeleteData(
                table: "wiki-fast-models",
                keyColumn: "Id",
                keyValue: "d28cb989669c46fba386666e84a2c72e");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("3a2370db-a80f-4bb4-86ba-37c71759c3c5"));

            migrationBuilder.DropColumn(
                name: "MaxTokensPerLine",
                table: "wiki-wiki-details");

            migrationBuilder.DropColumn(
                name: "MaxTokensPerParagraph",
                table: "wiki-wiki-details");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "wiki-wiki-details");

            migrationBuilder.DropColumn(
                name: "OverlappingTokens",
                table: "wiki-wiki-details");

            migrationBuilder.DropColumn(
                name: "QAPromptTemplate",
                table: "wiki-wiki-details");

            migrationBuilder.DropColumn(
                name: "TrainingPattern",
                table: "wiki-wiki-details");

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
    }
}
