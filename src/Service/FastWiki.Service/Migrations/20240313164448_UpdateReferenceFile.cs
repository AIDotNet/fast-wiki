using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReferenceFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("152edb28-ec98-4fe8-98e3-29b4487f3cc4"));

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceFile",
                table: "wiki-chat-dialog-history",
                type: "text",
                nullable: false,
                oldClrType: typeof(string[]),
                oldType: "text[]");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("0a36bba0-2bb5-4fbc-81da-997392e91c49"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 13, 16, 44, 47, 925, DateTimeKind.Utc).AddTicks(654), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 13, 16, 44, 47, 925, DateTimeKind.Utc).AddTicks(655), null, "admin", "f078dcf6acf7ecd387f1d4b346abd8bb", "13049809673", "5dba412d5db94106bbcc26b51fc1c8b4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("0a36bba0-2bb5-4fbc-81da-997392e91c49"));

            migrationBuilder.AlterColumn<string[]>(
                name: "ReferenceFile",
                table: "wiki-chat-dialog-history",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("152edb28-ec98-4fe8-98e3-29b4487f3cc4"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 13, 15, 58, 17, 901, DateTimeKind.Utc).AddTicks(4531), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 13, 15, 58, 17, 901, DateTimeKind.Utc).AddTicks(4533), null, "admin", "9d90012acf6c9d8f65539f9f525b9c23", "13049809673", "165fe60f29234634a7885f624b147964" });
        }
    }
}
