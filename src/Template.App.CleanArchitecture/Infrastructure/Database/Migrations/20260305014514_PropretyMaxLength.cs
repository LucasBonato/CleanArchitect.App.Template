using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.App.CleanArchitecture.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class PropretyMaxLength : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "last_name",
            schema: "public",
            table: "users",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            name: "first_name",
            schema: "public",
            table: "users",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            name: "email",
            schema: "public",
            table: "users",
            type: "character varying(200)",
            maxLength: 200,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            name: "description",
            schema: "public",
            table: "todo_items",
            type: "character varying(255)",
            maxLength: 255,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "last_name",
            schema: "public",
            table: "users",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(50)",
            oldMaxLength: 50);

        migrationBuilder.AlterColumn<string>(
            name: "first_name",
            schema: "public",
            table: "users",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(50)",
            oldMaxLength: 50);

        migrationBuilder.AlterColumn<string>(
            name: "email",
            schema: "public",
            table: "users",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            name: "description",
            schema: "public",
            table: "todo_items",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(255)",
            oldMaxLength: 255);
    }
}
