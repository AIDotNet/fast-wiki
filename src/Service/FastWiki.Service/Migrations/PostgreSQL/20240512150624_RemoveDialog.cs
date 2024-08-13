using Microsoft.EntityFrameworkCore.Migrations;

namespace FastWiki.Service.Migrations.PostgreSQL;

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
            new Guid("81ba41a1-27df-4f6c-b327-dfec1bce8c85"));

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-wikis",
            "character varying(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Model",
            "wiki-wikis",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Icon",
            "wiki-wikis",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "EmbeddingModel",
            "wiki-wikis",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Type",
            "wiki-wiki-details",
            "character varying(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Path",
            "wiki-wiki-details",
            "character varying(200)",
            maxLength: 200,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            "FileName",
            "wiki-wiki-details",
            "character varying(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Salt",
            "wiki-users",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Phone",
            "wiki-users",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Password",
            "wiki-users",
            "character varying(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-users",
            "character varying(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "Email",
            "wiki-users",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Avatar",
            "wiki-users",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Account",
            "wiki-users",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Parameters",
            "wiki-function-calls",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-function-calls",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Main",
            "wiki-function-calls",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Items",
            "wiki-function-calls",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Imports",
            "wiki-function-calls",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Description",
            "wiki-function-calls",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Content",
            "wiki-function-calls",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Path",
            "wiki-file-storages",
            "character varying(200)",
            maxLength: 200,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-file-storages",
            "character varying(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            "FullName",
            "wiki-file-storages",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-chat-share",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "ChatApplicationId",
            "wiki-chat-share",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "WikiIds",
            "wiki-chat-application",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Template",
            "wiki-chat-application",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Prompt",
            "wiki-chat-application",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Parameter",
            "wiki-chat-application",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Opener",
            "wiki-chat-application",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-chat-application",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "FunctionIds",
            "wiki-chat-application",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "Extend",
            "wiki-chat-application",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            "ChatModel",
            "wiki-chat-application",
            "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.CreateTable(
            "wiki-chat-record",
            table => new
            {
                Id = table.Column<string>("text", nullable: false),
                ApplicationId = table.Column<string>("text", nullable: true),
                Question = table.Column<string>("text", nullable: true),
                Creator = table.Column<Guid>("uuid", nullable: false),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: false),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-record", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-questions",
            table => new
            {
                Id = table.Column<string>("text", nullable: false),
                ApplicationId = table.Column<string>("text", nullable: true),
                Question = table.Column<string>("text", nullable: true),
                Order = table.Column<int>("integer", nullable: false),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false)
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
                new Guid("369059b8-6905-488b-87ab-447afa38b4de"), "admin",
                "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                new DateTime(2024, 5, 12, 15, 6, 23, 950, DateTimeKind.Utc).AddTicks(9764), null, "239573049@qq.com",
                false, false, new DateTime(2024, 5, 12, 15, 6, 23, 950, DateTimeKind.Utc).AddTicks(9767), null, "admin",
                "25b55175d0b2235c4fd324a0388343e0", "13049809673", 2, "c2f1bbebf79549178117880c43e58776"
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
            new Guid("369059b8-6905-488b-87ab-447afa38b4de"));

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-wikis",
            "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Model",
            "wiki-wikis",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Icon",
            "wiki-wikis",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "EmbeddingModel",
            "wiki-wikis",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Type",
            "wiki-wiki-details",
            "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Path",
            "wiki-wiki-details",
            "character varying(200)",
            maxLength: 200,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "FileName",
            "wiki-wiki-details",
            "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Salt",
            "wiki-users",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Phone",
            "wiki-users",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Password",
            "wiki-users",
            "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-users",
            "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Email",
            "wiki-users",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Avatar",
            "wiki-users",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Account",
            "wiki-users",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Parameters",
            "wiki-function-calls",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-function-calls",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Main",
            "wiki-function-calls",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Items",
            "wiki-function-calls",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Imports",
            "wiki-function-calls",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Description",
            "wiki-function-calls",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Content",
            "wiki-function-calls",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Path",
            "wiki-file-storages",
            "character varying(200)",
            maxLength: 200,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-file-storages",
            "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "FullName",
            "wiki-file-storages",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-chat-share",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "ChatApplicationId",
            "wiki-chat-share",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "WikiIds",
            "wiki-chat-application",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Template",
            "wiki-chat-application",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Prompt",
            "wiki-chat-application",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Parameter",
            "wiki-chat-application",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Opener",
            "wiki-chat-application",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Name",
            "wiki-chat-application",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "FunctionIds",
            "wiki-chat-application",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "Extend",
            "wiki-chat-application",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            "ChatModel",
            "wiki-chat-application",
            "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.CreateTable(
            "wiki-chat-dialog",
            table => new
            {
                Id = table.Column<string>("text", nullable: false),
                ApplicationId = table.Column<string>("text", nullable: false),
                ChatId = table.Column<string>("text", nullable: true),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Creator = table.Column<Guid>("uuid", nullable: false),
                Description = table.Column<string>("text", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: false),
                Name = table.Column<string>("text", nullable: false),
                Type = table.Column<int>("integer", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_wiki-chat-dialog", x => x.Id); });

        migrationBuilder.CreateTable(
            "wiki-chat-dialog-history",
            table => new
            {
                Id = table.Column<string>("text", nullable: false),
                ChatDialogId = table.Column<string>("text", nullable: false),
                Content = table.Column<string>("text", maxLength: -1, nullable: false),
                CreationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Creator = table.Column<Guid>("uuid", nullable: false),
                Current = table.Column<bool>("boolean", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
                ModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Modifier = table.Column<Guid>("uuid", nullable: false),
                ReferenceFile = table.Column<string>("text", nullable: false),
                TokenConsumption = table.Column<int>("integer", nullable: false),
                Type = table.Column<int>("integer", nullable: false)
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
                new Guid("81ba41a1-27df-4f6c-b327-dfec1bce8c85"), "admin",
                "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                new DateTime(2024, 4, 22, 14, 36, 39, 431, DateTimeKind.Utc).AddTicks(7635), null, "239573049@qq.com",
                false, false, new DateTime(2024, 4, 22, 14, 36, 39, 431, DateTimeKind.Utc).AddTicks(7638), null,
                "admin", "ae322a2c4c237df3b8683703e51442aa", "13049809673", 2, "19ea5ff625024a1d80bbf86c054e132c"
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