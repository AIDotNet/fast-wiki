using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddExtend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("289b14a4-a5e4-409f-97ec-049313593610"));

            migrationBuilder.AddColumn<string>(
                name: "Extend",
                table: "wiki-chat-application",
                type: "text",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Extend",
                table: "wiki-chat-application");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("289b14a4-a5e4-409f-97ec-049313593610"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 21, 14, 11, 35, 212, DateTimeKind.Utc).AddTicks(3429), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 21, 14, 11, 35, 212, DateTimeKind.Utc).AddTicks(3431), null, "admin", "75746e8a1cb14fed92c6688de29bd450", "13049809673", 1, "10604d18cf8e4475a622859a84ed5077" });
        }
    }
}
