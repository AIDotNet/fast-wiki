using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChatDialogChatId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("62769e54-0b0a-4a28-b3a5-b642c52ce8e5"));

            migrationBuilder.AlterColumn<string>(
                name: "ChatId",
                table: "wiki-chat-dialog",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "wiki-chat-dialog",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("abd7e154-ed32-4441-9c35-9fd6ab306e23"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 3, 4, 27, 49, 614, DateTimeKind.Utc).AddTicks(6846), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 3, 4, 27, 49, 614, DateTimeKind.Utc).AddTicks(6847), null, "admin", "138a300745acabff5c93f604c3a5df9c", "13049809673", "9c2182e49a034990a7730b7f80fbfd00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("abd7e154-ed32-4441-9c35-9fd6ab306e23"));

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "wiki-chat-dialog");

            migrationBuilder.AlterColumn<string>(
                name: "ChatId",
                table: "wiki-chat-dialog",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("62769e54-0b0a-4a28-b3a5-b642c52ce8e5"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 2, 29, 17, 59, 24, 403, DateTimeKind.Utc).AddTicks(1510), null, "239573049@qq.com", false, false, new DateTime(2024, 2, 29, 17, 59, 24, 403, DateTimeKind.Utc).AddTicks(1512), null, "admin", "a7eeba6911945963ce0eafa6352a7a89", "13049809673", "51111786de5e4809b4f72c9670f2c294" });
        }
    }
}
