using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class bookinginstanceticketid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "BookingInstance",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1b5b585-4b63-4063-b90c-ccbd82a5cb94", "AQAAAAEAACcQAAAAEEJgGi2JvTPOZ1hCLnDGRX1Tr8+V7cxYKGJK7sBsBVa/rMiegI0ZHpwMhsA4+XdPUA==", "5fb9685e-0185-4121-b308-f3a3f7c348ac" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingInstance_TicketId",
                table: "BookingInstance",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInstance_Ticket_TicketId",
                table: "BookingInstance",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInstance_Ticket_TicketId",
                table: "BookingInstance");

            migrationBuilder.DropIndex(
                name: "IX_BookingInstance_TicketId",
                table: "BookingInstance");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "BookingInstance");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6b5d527-96d8-4095-9f23-2c8d6b444782", "AQAAAAEAACcQAAAAEPHtrTZOiFfXAnLo42NThSdQFmsnLzlBrTkdw42BByRfdO3KkbSGpHPCvWdvQ3ENzg==", "8a349046-051b-48ec-b6d3-573df23fcac8" });
        }
    }
}
