using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_courseFolders_CourseFolderId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseFolderId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CourseFolderId",
                table: "Modules");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9858e05d-7a15-43aa-b3ed-5fbd9ace631f", "AQAAAAEAACcQAAAAEB0xy9ZdUXVspxfMevF3HqeaEXXBP76xIhY3aQK994KD4hhPsh9W2c1gD1AbwkHm4g==", "e1e8c0d6-fc3d-4c58-83bf-ddd1ed47babb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseFolderId",
                table: "Modules",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13831cb7-f2de-4cac-aed6-e12c890ef9cc", "AQAAAAEAACcQAAAAEEUlNbA/ILCL/WvWdR8Kx/k/UWmU/9+h1RslhdCoTLCo4tJNLNZoJsK96ddzr+iwNQ==", "a3c2b1ae-5b88-4a6d-8152-00c36eaf1d3f" });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseFolderId",
                table: "Modules",
                column: "CourseFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_courseFolders_CourseFolderId",
                table: "Modules",
                column: "CourseFolderId",
                principalTable: "courseFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
