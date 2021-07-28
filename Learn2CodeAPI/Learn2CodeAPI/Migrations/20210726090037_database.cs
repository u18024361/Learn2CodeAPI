using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentModule",
                table: "StudentModule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentModule",
                table: "StudentModule",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentModule",
                table: "StudentModule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentModule",
                table: "StudentModule",
                columns: new[] { "StudentId", "ModuleId" });
        }
    }
}
