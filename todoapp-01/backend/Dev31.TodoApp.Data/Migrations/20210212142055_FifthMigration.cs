using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace Dev31.TodoApp.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Entry",
                table: "Tasks",
                type: "nvarchar(27)",
                maxLength: 27,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(27)",
                oldMaxLength: 27);

            migrationBuilder.AddColumn<int>(
                name: "TodoTaskId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TodoTaskId",
                table: "Tags",
                column: "TodoTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Tasks_TodoTaskId",
                table: "Tags",
                column: "TodoTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Tasks_TodoTaskId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TodoTaskId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TodoTaskId",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Entry",
                table: "Tasks",
                type: "nvarchar(27)",
                maxLength: 27,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(27)",
                oldMaxLength: 27,
                oldNullable: true);
        }
    }
}
