using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddWikiState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("abd7e154-ed32-4441-9c35-9fd6ab306e23"));

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "wiki-wiki-details",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("d2b220f9-ec98-4d9e-be8e-d4247c62cd1f"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 3, 5, 1, 46, 361, DateTimeKind.Utc).AddTicks(8631), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 3, 5, 1, 46, 361, DateTimeKind.Utc).AddTicks(8633), null, "admin", "86267b268fed10b00b0f111e0aabc0ec", "13049809673", "0ec17a397853473ebb701bba84cf9228" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("d2b220f9-ec98-4d9e-be8e-d4247c62cd1f"));

            migrationBuilder.DropColumn(
                name: "State",
                table: "wiki-wiki-details");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Salt" },
                values: new object[] { new Guid("abd7e154-ed32-4441-9c35-9fd6ab306e23"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 3, 3, 4, 27, 49, 614, DateTimeKind.Utc).AddTicks(6846), null, "239573049@qq.com", false, false, new DateTime(2024, 3, 3, 4, 27, 49, 614, DateTimeKind.Utc).AddTicks(6847), null, "admin", "138a300745acabff5c93f604c3a5df9c", "13049809673", "9c2182e49a034990a7730b7f80fbfd00" });
        }
    }
}
