using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class RegisteredStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotesName",
                table: "GroupSessionContent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordingName",
                table: "GroupSessionContent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegisteredStudent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    BookingInstanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisteredStudent_BookingInstance_BookingInstanceId",
                        column: x => x.BookingInstanceId,
                        principalTable: "BookingInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisteredStudent_Students_StudentId",
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
                values: new object[] { "9397d547-44b5-4efe-9934-038513f404f9", "AQAAAAEAACcQAAAAEEaTKNajW8bgvvPeD8rvRR5Ch48ou94gGzT627XcT7HlXQVKX/oLTCU6QqR673Ce0w==", "f9f51c57-4e73-42e2-abc0-ffc73c3c1900" });

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredStudent_BookingInstanceId",
                table: "RegisteredStudent",
                column: "BookingInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredStudent_StudentId",
                table: "RegisteredStudent",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisteredStudent");

            migrationBuilder.DropColumn(
                name: "NotesName",
                table: "GroupSessionContent");

            migrationBuilder.DropColumn(
                name: "RecordingName",
                table: "GroupSessionContent");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21c4d925-bed3-4fbe-8e11-e8fc74c5b08c", "AQAAAAEAACcQAAAAEOIj8E3TYBenbFfDuN4KykWkLNJFHjq6srtPWdaj1UDnS7MDTReSrxb6WTsrXtpGZg==", "4b7bd439-6495-4e21-b4da-f2be3bfb9f8f" });
        }
    }
}
