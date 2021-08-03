using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class courseContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filepath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseSubCategoryId = table.Column<int>(type: "int", nullable: false),
                    ContentTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseContent_ContentType_ContentTypeId",
                        column: x => x.ContentTypeId,
                        principalTable: "ContentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseContent_courseSubCategory_CourseSubCategoryId",
                        column: x => x.CourseSubCategoryId,
                        principalTable: "courseSubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bef04d7-8f20-4625-8da1-1155f6642f7b", "AQAAAAEAACcQAAAAEDIXgQWswJ4c21O++4kQ1wicdngeXmw8MD82WzqnADnn6vW+8K9clZNvpGWZ/hE9/A==", "9b2d06b8-90a7-4534-826a-a991c22229a6" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseContent_ContentTypeId",
                table: "CourseContent",
                column: "ContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContent_CourseSubCategoryId",
                table: "CourseContent",
                column: "CourseSubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseContent");

            migrationBuilder.DropTable(
                name: "ContentType");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2928804a-3fe9-4f63-9ef3-1c1f20a32069", "AQAAAAEAACcQAAAAELZ+nY2cxiPB8pskNIwmzbK1/CzYy3iebcwKd70ZQs8B3Y5wa7pmUOchEEAssu4ZNA==", "09bacfae-8f8d-4252-a4a9-913ebe4a4b9e" });
        }
    }
}
