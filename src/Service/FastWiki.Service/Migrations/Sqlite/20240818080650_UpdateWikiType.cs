using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastWiki.Service.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class UpdateWikiType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_wiki-questions_CreationTime",
                table: "wiki-questions");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("e45c6b3f-70b4-4ba2-a992-2179607d752b"));

            migrationBuilder.AddColumn<int>(
                name: "VectorType",
                table: "wiki-wikis",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "wiki-histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MemoryId = table.Column<string>(type: "TEXT", nullable: false),
                    PrevValue = table.Column<string>(type: "TEXT", nullable: false),
                    NewValue = table.Column<string>(type: "TEXT", nullable: false),
                    Event = table.Column<string>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    TrackId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wiki-histories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("c90e7051-59b1-43c0-9786-41858f59e52b"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 8, 18, 8, 6, 50, 493, DateTimeKind.Utc).AddTicks(7283), null, "239573049@qq.com", false, false, new DateTime(2024, 8, 18, 8, 6, 50, 493, DateTimeKind.Utc).AddTicks(7296), null, "admin", "b2993bf179ad3859601be08f3bbc13b8", "13049809673", 2, "f0b717df397f4504a64e5cdb1790c110" });

            migrationBuilder.CreateIndex(
                name: "IX_wiki-histories_MemoryId",
                table: "wiki-histories",
                column: "MemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-histories_TrackId",
                table: "wiki-histories",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_wiki-histories_UserId",
                table: "wiki-histories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wiki-histories");

            migrationBuilder.DeleteData(
                table: "wiki-users",
                keyColumn: "Id",
                keyValue: new Guid("c90e7051-59b1-43c0-9786-41858f59e52b"));

            migrationBuilder.DropColumn(
                name: "VectorType",
                table: "wiki-wikis");

            migrationBuilder.InsertData(
                table: "wiki-users",
                columns: new[] { "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable", "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt" },
                values: new object[] { new Guid("e45c6b3f-70b4-4ba2-a992-2179607d752b"), "admin", "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", new DateTime(2024, 5, 12, 14, 58, 27, 64, DateTimeKind.Utc).AddTicks(7298), null, "239573049@qq.com", false, false, new DateTime(2024, 5, 12, 14, 58, 27, 64, DateTimeKind.Utc).AddTicks(7301), null, "admin", "40237cc3bff510e141de01a3f036be71", "13049809673", 2, "445acb533eca439b90ebd887084438ab" });

            migrationBuilder.CreateIndex(
                name: "IX_wiki-questions_CreationTime",
                table: "wiki-questions",
                column: "CreationTime");
        }
    }
}
