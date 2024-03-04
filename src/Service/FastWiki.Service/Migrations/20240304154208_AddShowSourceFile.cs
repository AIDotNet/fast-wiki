using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddShowSourceFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("2d1e2010-20df-4493-8dbb-5877a2636249"));

            migrationBuilder.AddColumn<bool>(
                name: "ShowSourceFile",
                table: "wiki-chat-application",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("358be106-ef0d-48f6-8080-8dd9baa70bb0"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 4, 15, 42, 8, 623, DateTimeKind.Utc).AddTicks(9490), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 4, 15, 42, 8, 623, DateTimeKind.Utc).AddTicks(9493), null, "admin", "6e9fb78da16160f9f4b15dd5fd32c015", "13049809673", "d865425680544e0589022eae5de3ea0f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("358be106-ef0d-48f6-8080-8dd9baa70bb0"));

            migrationBuilder.DropColumn(
                name: "ShowSourceFile",
                table: "wiki-chat-application");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("2d1e2010-20df-4493-8dbb-5877a2636249"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 4, 15, 7, 11, 399, DateTimeKind.Utc).AddTicks(9028), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 4, 15, 7, 11, 399, DateTimeKind.Utc).AddTicks(9029), null, "admin", "796f1fe636b48af22a2200923b2cb81b", "13049809673", "60e7948e5ff44bd48929fa5186360cf4" });
        }
    }
}
