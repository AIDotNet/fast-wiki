using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddNoReplyFound : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("55006517-7761-41e4-9405-dd552607d869"));

            migrationBuilder.AddColumn<string>(
                name: "NoReplyFoundTemplate",
                table: "wiki-chat-application",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("2d1e2010-20df-4493-8dbb-5877a2636249"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 4, 15, 7, 11, 399, DateTimeKind.Utc).AddTicks(9028), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 4, 15, 7, 11, 399, DateTimeKind.Utc).AddTicks(9029), null, "admin", "796f1fe636b48af22a2200923b2cb81b", "13049809673", "60e7948e5ff44bd48929fa5186360cf4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("2d1e2010-20df-4493-8dbb-5877a2636249"));

            migrationBuilder.DropColumn(
                name: "NoReplyFoundTemplate",
                table: "wiki-chat-application");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("55006517-7761-41e4-9405-dd552607d869"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 4, 11, 19, 44, 683, DateTimeKind.Utc).AddTicks(2439), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 4, 11, 19, 44, 683, DateTimeKind.Utc).AddTicks(2441), null, "admin", "7f38e71118db76e732b6060705eeb447", "13049809673", "70f30ad83f6140c68120304f1300b032" });
        }
    }
}
