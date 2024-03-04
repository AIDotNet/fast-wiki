using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddEmbeddingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("d2b220f9-ec98-4d9e-be8e-d4247c62cd1f"));

            migrationBuilder.AddColumn<string>(
                name: "EmbeddingModel",
                table: "wiki-wikis",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("55006517-7761-41e4-9405-dd552607d869"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 4, 11, 19, 44, 683, DateTimeKind.Utc).AddTicks(2439), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 4, 11, 19, 44, 683, DateTimeKind.Utc).AddTicks(2441), null, "admin", "7f38e71118db76e732b6060705eeb447", "13049809673", "70f30ad83f6140c68120304f1300b032" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("55006517-7761-41e4-9405-dd552607d869"));

            migrationBuilder.DropColumn(
                name: "EmbeddingModel",
                table: "wiki-wikis");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("d2b220f9-ec98-4d9e-be8e-d4247c62cd1f"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 3, 5, 1, 46, 361, DateTimeKind.Utc).AddTicks(8631), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 3, 5, 1, 46, 361, DateTimeKind.Utc).AddTicks(8633), null, "admin", "86267b268fed10b00b0f111e0aabc0ec", "13049809673", "0ec17a397853473ebb701bba84cf9228" });
        }
    }
}
