using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class booking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "BookingInstance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_Students_StudentId",
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
                values: new object[] { "85f46c78-9071-411c-a8d6-6f39a151ffc8", "AQAAAAEAACcQAAAAEIXEy5/iIvQfDV+ApUDgGItZMsji2SrwRGsyQdsq/BXCb1LLo48UxpH/pyBzPsKaWw==", "8f70599c-ef3e-47c5-981c-410a51eaf173" });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ModuleId",
                table: "Ticket",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingInstance_BookingId",
                table: "BookingInstance",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_StudentId",
                table: "Booking",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInstance_Booking_BookingId",
                table: "BookingInstance",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Modules_ModuleId",
                table: "Ticket",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInstance_Booking_BookingId",
                table: "BookingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Modules_ModuleId",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_ModuleId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_BookingInstance_BookingId",
                table: "BookingInstance");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "BookingInstance");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88243897-75be-4739-8ff9-63797a0d79cb", "AQAAAAEAACcQAAAAEFNC8vhbN0XOYKOOFE7l8HqYdDXY7Xp9qYbanb7iIN79LWA56YkjTfdStR0hETKAUQ==", "5707317a-e1cb-46e4-8841-40201ae048dd" });
        }
    }
}
