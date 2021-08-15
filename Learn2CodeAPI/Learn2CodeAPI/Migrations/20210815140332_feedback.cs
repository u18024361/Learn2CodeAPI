using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class feedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Friendliness = table.Column<int>(type: "int", nullable: false),
                    Timliness = table.Column<int>(type: "int", nullable: false),
                    Ability = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    BookingInstanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_BookingInstance_BookingInstanceId",
                        column: x => x.BookingInstanceId,
                        principalTable: "BookingInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedback_Students_StudentId",
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
                values: new object[] { "cf341842-c60b-4234-8da4-bb757a6f41f4", "AQAAAAEAACcQAAAAEE30i7yiiELpfyz+C6EZIN+7jL6zQa8CYMAdrca9Gcb9pprnjjwVJYoE4TUmWq4pNw==", "080ddfc3-1061-4f27-b0c2-f81dd629166f" });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_BookingInstanceId",
                table: "Feedback",
                column: "BookingInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_StudentId",
                table: "Feedback",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1b5b585-4b63-4063-b90c-ccbd82a5cb94", "AQAAAAEAACcQAAAAEEJgGi2JvTPOZ1hCLnDGRX1Tr8+V7cxYKGJK7sBsBVa/rMiegI0ZHpwMhsA4+XdPUA==", "5fb9685e-0185-4121-b308-f3a3f7c348ac" });
        }
    }
}
