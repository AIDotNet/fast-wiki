using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFunctionItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "Cotnent",
                table: "wiki-function-calls",
                newName: "Main");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "wiki-function-calls",
                type: "text",
                nullable: false,
                defaultValue: "");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Content",
                table: "wiki-function-calls");

            migrationBuilder.RenameColumn(
                name: "Main",
                table: "wiki-function-calls",
                newName: "Cotnent");

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
        }
    }
}
