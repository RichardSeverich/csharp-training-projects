using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;


namespace Dev31.TodoApp.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "TaskTags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TaskTags",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
