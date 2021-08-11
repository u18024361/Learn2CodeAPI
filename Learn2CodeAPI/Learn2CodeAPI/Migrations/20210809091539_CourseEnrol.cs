using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class CourseEnrol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseBasketLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    CourseSubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseBasketLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseBasketLine_Basket_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Basket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseBasketLine_courseSubCategory_CourseSubCategoryId",
                        column: x => x.CourseSubCategoryId,
                        principalTable: "courseSubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEnrol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseEnrol_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEnrolLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseEnrolId = table.Column<int>(type: "int", nullable: false),
                    CourseSubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrolLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseEnrolLine_CourseEnrol_CourseEnrolId",
                        column: x => x.CourseEnrolId,
                        principalTable: "CourseEnrol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseEnrolLine_courseSubCategory_CourseSubCategoryId",
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
                values: new object[] { "00faab49-16b7-4b65-a92e-ee63278052f1", "AQAAAAEAACcQAAAAEABUa5gmd2XCjA++qkeUw0Er5i8SzaaGlBuhSWPwmZOJMesGRvqyXhtZvoSWfqG+Fw==", "5a6acd09-69cc-4e16-aefb-08c7e98ee14f" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseBasketLine_BasketId",
                table: "CourseBasketLine",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBasketLine_CourseSubCategoryId",
                table: "CourseBasketLine",
                column: "CourseSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrol_StudentId",
                table: "CourseEnrol",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrolLine_CourseEnrolId",
                table: "CourseEnrolLine",
                column: "CourseEnrolId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrolLine_CourseSubCategoryId",
                table: "CourseEnrolLine",
                column: "CourseSubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseBasketLine");

            migrationBuilder.DropTable(
                name: "CourseEnrolLine");

            migrationBuilder.DropTable(
                name: "CourseEnrol");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "431ed86f-a179-43b2-93d8-a7e453099bf2", "AQAAAAEAACcQAAAAEHri2XC0zkcoF85pc+z8oqadXE+mWmrETADQIZrMvSMoUEfypchwNpIIRcWRldFcOQ==", "d315acc1-85a0-4e94-aee8-dea2f86b7486" });
        }
    }
}
