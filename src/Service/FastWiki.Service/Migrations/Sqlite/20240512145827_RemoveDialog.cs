using Microsoft.EntityFrameworkCore.Migrations;

namespace FastWiki.Service.Migrations.Sqlite;

/// <inheritdoc />
public partial class RemoveDialog : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "wiki-chat-dialog");

        migrationBuilder.DropTable(
            "wiki-chat-dialog-history");

        migrationBuilder.DeleteData(
            "wiki-users",
            "Id",
            new Guid("c8b3b44a-6e7c-4f11-88b2-ed846e953b7c"));

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-wikis",
            "TEXT",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Model",
            "wiki-wikis",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Icon",
            "wiki-wikis",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "EmbeddingModel",
            "wiki-wikis",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Type",
            "wiki-wiki-details",
            "TEXT",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Path",
            "wiki-wiki-details",
            "TEXT",
            maxLength: 200,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            "FileName",
            "wiki-wiki-details",
            "TEXT",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Salt",
            "wiki-users",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Phone",
            "wiki-users",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Password",
            "wiki-users",
            "TEXT",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-users",
            "TEXT",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Email",
            "wiki-users",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Avatar",
            "wiki-users",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Account",
            "wiki-users",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Parameters",
            "wiki-function-calls",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-function-calls",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Main",
            "wiki-function-calls",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Items",
            "wiki-function-calls",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Imports",
            "wiki-function-calls",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Description",
            "wiki-function-calls",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Content",
            "wiki-function-calls",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Path",
            "wiki-file-storages",
            "TEXT",
            maxLength: 200,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-file-storages",
            "TEXT",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "FullName",
            "wiki-file-storages",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-chat-share",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "ChatApplicationId",
            "wiki-chat-share",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "WikiIds",
            "wiki-chat-application",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Template",
            "wiki-chat-application",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Prompt",
            "wiki-chat-application",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Parameter",
            "wiki-chat-application",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Opener",
            "wiki-chat-application",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-chat-application",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "FunctionIds",
            "wiki-chat-application",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "Extend",
            "wiki-chat-application",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            "ChatModel",
            "wiki-chat-application",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.CreateTable(
            "wiki-chat-record",
            table => new
            {
                Id = table.Column<string>("TEXT", nullable: false),
                ApplicationId = table.Column<string>("TEXT", nullable: true),
                Question = table.Column<string>("TEXT", nullable: true),
                Creator = table.Column<Guid>("TEXT", nullable: false),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: false),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-record", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-questions",
            table => new
            {
                Id = table.Column<string>("TEXT", nullable: false),
                ApplicationId = table.Column<string>("TEXT", nullable: true),
                Question = table.Column<string>("TEXT", nullable: true),
                Order = table.Column<int>("INTEGER", nullable: false),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-questions", x => x.Id); });

        migrationBuilder.InsertData(
            "wiki-users",
            new[]
            {
                "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable",
                "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt"
            },
            new object[]
            {
                new Guid("e45c6b3f-70b4-4ba2-a992-2179607d752b"), "admin",
                "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                new DateTime(2024, 5, 12, 14, 58, 27, 64, DateTimeKind.Utc).AddTicks(7298), null, "239573049@qq.com",
                false, false, new DateTime(2024, 5, 12, 14, 58, 27, 64, DateTimeKind.Utc).AddTicks(7301), null, "admin",
                "40237cc3bff510e141de01a3f036be71", "13049809673", 2, "445acb533eca439b90ebd887084438ab"
            });

        migrationBuilder.CreateIndex(
            "IX_wiki-chat-record_CreationTime",
            "wiki-chat-record",
            "CreationTime");

        migrationBuilder.CreateIndex(
            "IX_wiki-questions_CreationTime",
            "wiki-questions",
            "CreationTime");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "wiki-chat-record");

        migrationBuilder.DropTable(
            "wiki-questions");

        migrationBuilder.DeleteData(
            "wiki-users",
            "Id",
            new Guid("e45c6b3f-70b4-4ba2-a992-2179607d752b"));

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-wikis",
            "TEXT",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Model",
            "wiki-wikis",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Icon",
            "wiki-wikis",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "EmbeddingModel",
            "wiki-wikis",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Type",
            "wiki-wiki-details",
            "TEXT",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Path",
            "wiki-wiki-details",
            "TEXT",
            maxLength: 200,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 200,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "FileName",
            "wiki-wiki-details",
            "TEXT",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Salt",
            "wiki-users",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Phone",
            "wiki-users",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Password",
            "wiki-users",
            "TEXT",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-users",
            "TEXT",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Email",
            "wiki-users",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Avatar",
            "wiki-users",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Account",
            "wiki-users",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Parameters",
            "wiki-function-calls",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-function-calls",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Main",
            "wiki-function-calls",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Items",
            "wiki-function-calls",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Imports",
            "wiki-function-calls",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Description",
            "wiki-function-calls",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Content",
            "wiki-function-calls",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Path",
            "wiki-file-storages",
            "TEXT",
            maxLength: 200,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 200,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-file-storages",
            "TEXT",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "FullName",
            "wiki-file-storages",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-chat-share",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "ChatApplicationId",
            "wiki-chat-share",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "WikiIds",
            "wiki-chat-application",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Template",
            "wiki-chat-application",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Prompt",
            "wiki-chat-application",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Parameter",
            "wiki-chat-application",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Opener",
            "wiki-chat-application",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-chat-application",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "FunctionIds",
            "wiki-chat-application",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Extend",
            "wiki-chat-application",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "ChatModel",
            "wiki-chat-application",
            "TEXT",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.CreateTable(
            "wiki-chat-dialog",
            table => new
            {
                Id = table.Column<string>("TEXT", nullable: false),
                ApplicationId = table.Column<string>("TEXT", nullable: false),
                ChatId = table.Column<string>("TEXT", nullable: true),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Creator = table.Column<Guid>("TEXT", nullable: false),
                Description = table.Column<string>("TEXT", nullable: false),
                IsDeleted = table.Column<bool>("INTEGER", nullable: false),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", nullable: false),
                Type = table.Column<int>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-dialog", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-chat-dialog-history",
            table => new
            {
                Id = table.Column<string>("TEXT", nullable: false),
                ChatDialogId = table.Column<string>("TEXT", nullable: false),
                Content = table.Column<string>("TEXT", maxLength: -1, nullable: false),
                CreationTime = table.Column<DateTime>("TEXT", nullable: false),
                Creator = table.Column<Guid>("TEXT", nullable: false),
                Current = table.Column<bool>("INTEGER", nullable: false),
                IsDeleted = table.Column<bool>("INTEGER", nullable: false),
                ModificationTime = table.Column<DateTime>("TEXT", nullable: false),
                Modifier = table.Column<Guid>("TEXT", nullable: false),
                ReferenceFile = table.Column<string>("TEXT", nullable: false),
                TokenConsumption = table.Column<int>("INTEGER", nullable: false),
                Type = table.Column<int>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-dialog-history", x => x.Id); });

        migrationBuilder.InsertData(
            "wiki-users",
            new[]
            {
                "Id", "Account", "Avatar", "CreationTime", "Creator", "Email", "IsDeleted", "IsDisable",
                "ModificationTime", "Modifier", "Name", "Password", "Phone", "Role", "Salt"
            },
            new object[]
            {
                new Guid("c8b3b44a-6e7c-4f11-88b2-ed846e953b7c"), "admin",
                "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                new DateTime(2024, 4, 22, 14, 44, 40, 964, DateTimeKind.Utc).AddTicks(8730), null, "239573049@qq.com",
                false, false, new DateTime(2024, 4, 22, 14, 44, 40, 964, DateTimeKind.Utc).AddTicks(8733), null,
                "admin", "418817cad9f0eae28501500c5a6e31e9", "13049809673", 2, "06a074ae10a44f2ea7b0de027562723e"
            });

        migrationBuilder.CreateIndex(
            "IX_wiki-chat-dialog_ChatId",
            "wiki-chat-dialog",
            "ChatId");

        migrationBuilder.CreateIndex(
            "IX_wiki-chat-dialog-history_ChatDialogId",
            "wiki-chat-dialog-history",
            "ChatDialogId");

        migrationBuilder.CreateIndex(
            "IX_wiki-chat-dialog-history_Creator",
            "wiki-chat-dialog-history",
            "Creator");
    }
}