using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseFolders_courseFolders_CourseFolderId",
                table: "courseFolders");

            migrationBuilder.DropIndex(
                name: "IX_courseFolders_CourseFolderId",
                table: "courseFolders");

            migrationBuilder.DropColumn(
                name: "CourseFolderId",
                table: "courseFolders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseFolderId",
                table: "courseFolders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_courseFolders_CourseFolderId",
                table: "courseFolders",
                column: "CourseFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_courseFolders_courseFolders_CourseFolderId",
                table: "courseFolders",
                column: "CourseFolderId",
                principalTable: "courseFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
