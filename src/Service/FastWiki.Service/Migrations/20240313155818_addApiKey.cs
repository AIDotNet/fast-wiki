using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class addApiKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("358be106-ef0d-48f6-8080-8dd9baa70bb0"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                table: "wiki-chat-share",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "APIKey",
                table: "wiki-chat-share",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsedToken",
                table: "wiki-chat-share",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string[]>(
                name: "ReferenceFile",
                table: "wiki-chat-dialog-history",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("152edb28-ec98-4fe8-98e3-29b4487f3cc4"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 13, 15, 58, 17, 901, DateTimeKind.Utc).AddTicks(4531), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 13, 15, 58, 17, 901, DateTimeKind.Utc).AddTicks(4533), null, "admin", "9d90012acf6c9d8f65539f9f525b9c23", "13049809673", "165fe60f29234634a7885f624b147964" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("152edb28-ec98-4fe8-98e3-29b4487f3cc4"));

            migrationBuilder.DropColumn(
                name: "APIKey",
                table: "wiki-chat-share");

            migrationBuilder.DropColumn(
                name: "UsedToken",
                table: "wiki-chat-share");

            migrationBuilder.DropColumn(
                name: "ReferenceFile",
                table: "wiki-chat-dialog-history");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                table: "wiki-chat-share",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("358be106-ef0d-48f6-8080-8dd9baa70bb0"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 4, 15, 42, 8, 623, DateTimeKind.Utc).AddTicks(9490), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 4, 15, 42, 8, 623, DateTimeKind.Utc).AddTicks(9493), null, "admin", "6e9fb78da16160f9f4b15dd5fd32c015", "13049809673", "d865425680544e0589022eae5de3ea0f" });
        }
    }
}
