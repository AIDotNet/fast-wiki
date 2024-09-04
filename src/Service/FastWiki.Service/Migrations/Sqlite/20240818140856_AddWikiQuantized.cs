using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class AddWikiQuantized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("c90e7051-59b1-43c0-9786-41858f59e52b"));

            migrationBuilder.CreateTable(
                name: "wiki-quantized-lists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WikiId = table.Column<long>(type: "INTEGER", nullable: false),
                    WikiDetailId = table.Column<long>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true),
                    ProcessTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-quantized-lists", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("6206211d-6e07-42ea-8750-7d415a2ffaa8"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 8, 18, 14, 8, 56, 546, DateTimeKind.Utc).AddTicks(8987), null, "239573049@qq.com", false, false, new DateTime(2024, 8, 18, 14, 8, 56, 546, DateTimeKind.Utc).AddTicks(8990), null, "admin", "f0f6d913839798fff09760727a015264", "13049809673", 2, "ff4cdcea6d814b80a4777acf20cdc52f" });

            migrationBuilder.CreateIndex(
                name: "IX_wiki-quantized-lists_WikiDetailId",
                table: "wiki-quantized-lists",
                column: "WikiDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-quantized-lists_WikiId",
                table: "wiki-quantized-lists",
                column: "WikiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wiki-quantized-lists");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("6206211d-6e07-42ea-8750-7d415a2ffaa8"));

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("c90e7051-59b1-43c0-9786-41858f59e52b"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 8, 18, 8, 6, 50, 493, DateTimeKind.Utc).AddTicks(7283), null, "239573049@qq.com", false, false, new DateTime(2024, 8, 18, 8, 6, 50, 493, DateTimeKind.Utc).AddTicks(7296), null, "admin", "b2993bf179ad3859601be08f3bbc13b8", "13049809673", 2, "f0b717df397f4504a64e5cdb1790c110" });
        }
    }
}
