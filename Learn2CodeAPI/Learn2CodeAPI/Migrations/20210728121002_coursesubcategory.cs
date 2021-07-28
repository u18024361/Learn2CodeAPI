using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class coursesubcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseFolderId",
                table: "Modules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "courseSubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseSubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<double>(type: "float", nullable: false),
                    CourseFolderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courseSubCategory_courseFolders_CourseFolderId",
                        column: x => x.CourseFolderId,
                        principalTable: "courseFolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseFolderId",
                table: "Modules",
                column: "CourseFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_courseSubCategory_CourseFolderId",
                table: "courseSubCategory",
                column: "CourseFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_courseFolders_CourseFolderId",
                table: "Modules",
                column: "CourseFolderId",
                principalTable: "courseFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_courseFolders_CourseFolderId",
                table: "Modules");

            migrationBuilder.DropTable(
                name: "courseSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseFolderId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CourseFolderId",
                table: "Modules");
        }
    }
}
