using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace Dev31.TodoApp.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
