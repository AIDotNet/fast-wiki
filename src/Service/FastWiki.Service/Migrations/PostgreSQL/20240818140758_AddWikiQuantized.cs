using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations.PostgreSQL
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
                keyValue: new Guid("3667f5ec-65d4-4543-84cd-00a246c77215"));

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
                values: new object[] { new Guid("c61a32ee-9254-4018-a34d-6df4c9269946"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 8, 18, 14, 7, 58, 699, DateTimeKind.Utc).AddTicks(6364), null, "239573049@qq.com", false, false, new DateTime(2024, 8, 18, 14, 7, 58, 699, DateTimeKind.Utc).AddTicks(6367), null, "admin", "8b0378cf4c257b662603ccf76cc61d29", "13049809673", 2, "37c8e92134974dc4b28d875436da2afd" });

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
                keyValue: new Guid("c61a32ee-9254-4018-a34d-6df4c9269946"));

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("3667f5ec-65d4-4543-84cd-00a246c77215"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 8, 18, 8, 7, 12, 702, DateTimeKind.Utc).AddTicks(9179), null, "239573049@qq.com", false, false, new DateTime(2024, 8, 18, 8, 7, 12, 702, DateTimeKind.Utc).AddTicks(9183), null, "admin", "6726a2a303e85cffa4eb309d66715aee", "13049809673", 2, "93b6fe5a96f94c64b7ddff515a007e2f" });
        }
    }
}
