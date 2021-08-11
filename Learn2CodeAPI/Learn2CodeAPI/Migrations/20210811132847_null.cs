using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class @null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInstance_Booking_BookingId",
                table: "BookingInstance");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "BookingInstance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f7e14a8-496d-477d-b504-d4c7eec1cfe2", "AQAAAAEAACcQAAAAEMGGKgSQI7aZl4YmMzb7aQU2rCxTvlIQqTJHePssXL7SHmquZDcohU/1twphmBoi0A==", "86c0259b-464e-4430-9d18-8b22df2130fa" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInstance_Booking_BookingId",
                table: "BookingInstance",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInstance_Booking_BookingId",
                table: "BookingInstance");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "BookingInstance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85f46c78-9071-411c-a8d6-6f39a151ffc8", "AQAAAAEAACcQAAAAEIXEy5/iIvQfDV+ApUDgGItZMsji2SrwRGsyQdsq/BXCb1LLo48UxpH/pyBzPsKaWw==", "8f70599c-ef3e-47c5-981c-410a51eaf173" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInstance_Booking_BookingId",
                table: "BookingInstance",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
