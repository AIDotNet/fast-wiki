using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("0a36bba0-2bb5-4fbc-81da-997392e91c49"));

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "wiki-users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("2bef435d-fe59-4f7a-8a8a-2939c08dbeed"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 14, 17, 52, 13, 176, DateTimeKind.Utc).AddTicks(7297), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 14, 17, 52, 13, 176, DateTimeKind.Utc).AddTicks(7299), null, "admin", "0eae0a1d2cc30beefde867611cb04904", "13049809673", 2, "0c9e851a82e9434085e20cfb3818319a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("2bef435d-fe59-4f7a-8a8a-2939c08dbeed"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "wiki-users");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("0a36bba0-2bb5-4fbc-81da-997392e91c49"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 13, 16, 44, 47, 925, DateTimeKind.Utc).AddTicks(654), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 13, 16, 44, 47, 925, DateTimeKind.Utc).AddTicks(655), null, "admin", "f078dcf6acf7ecd387f1d4b346abd8bb", "13049809673", "5dba412d5db94106bbcc26b51fc1c8b4" });
        }
    }
}
