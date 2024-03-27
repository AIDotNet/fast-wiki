using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddChatType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("2bef435d-fe59-4f7a-8a8a-2939c08dbeed"));

            migrationBuilder.AddColumn<string>(
                name: "ChatType",
                table: "wiki-chat-application",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("a899e701-4d58-4809-843f-a024e3bf629c"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 19, 17, 57, 2, 871, DateTimeKind.Utc).AddTicks(3601), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 19, 17, 57, 2, 871, DateTimeKind.Utc).AddTicks(3603), null, "admin", "2f204309fb0afa4c2bdba7b2904ea10e", "13049809673", 2, "ab3b086ec59a45cfbf65398ad8f64fdb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("a899e701-4d58-4809-843f-a024e3bf629c"));

            migrationBuilder.DropColumn(
                name: "ChatType",
                table: "wiki-chat-application");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("2bef435d-fe59-4f7a-8a8a-2939c08dbeed"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 14, 17, 52, 13, 176, DateTimeKind.Utc).AddTicks(7297), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 14, 17, 52, 13, 176, DateTimeKind.Utc).AddTicks(7299), null, "admin", "0eae0a1d2cc30beefde867611cb04904", "13049809673", 2, "0c9e851a82e9434085e20cfb3818319a" });
        }
    }
}
