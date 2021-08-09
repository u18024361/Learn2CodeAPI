using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class Basket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Basket_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "431ed86f-a179-43b2-93d8-a7e453099bf2", "AQAAAAEAACcQAAAAEHri2XC0zkcoF85pc+z8oqadXE+mWmrETADQIZrMvSMoUEfypchwNpIIRcWRldFcOQ==", "d315acc1-85a0-4e94-aee8-dea2f86b7486" });

            migrationBuilder.CreateIndex(
                name: "IX_Basket_StudentId",
                table: "Basket",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89ee56cd-3f6f-4e6e-8526-828162cfdeb6", "AQAAAAEAACcQAAAAEGNuYz718vVLsH2kRMNVISuLNc0tmxlnNY8FSWQqe0bI1gnBk0O7VxYBUlyEiouTQQ==", "945a7d5b-9608-4c91-a09e-7d2727956f05" });
        }
    }
}
