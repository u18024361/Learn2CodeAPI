using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Tutor_TutorId",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_TutorId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "File");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Tutor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tutor_FileId",
                table: "Tutor",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutor_File_FileId",
                table: "Tutor",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutor_File_FileId",
                table: "Tutor");

            migrationBuilder.DropIndex(
                name: "IX_Tutor_FileId",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Tutor");

            migrationBuilder.AddColumn<int>(
                name: "TutorId",
                table: "File",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_File_TutorId",
                table: "File",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Tutor_TutorId",
                table: "File",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
