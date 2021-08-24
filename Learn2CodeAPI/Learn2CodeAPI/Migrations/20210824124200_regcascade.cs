using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class regcascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_RegisteredStudent_Students_StudentId",
            table: "RegisteredStudent");
            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredStudent_Students_StudentId",
                table: "RegisteredStudent",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
           name: "FK_RegisteredStudent_Students_StudentId",
           table: "RegisteredStudent");
            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredStudent_Students_StudentId",
                table: "RegisteredStudent",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }




    }
}
