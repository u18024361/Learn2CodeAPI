using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class ticketenroline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrolLineId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnrolLineId",
                table: "EnrolLine",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "858448dc-1524-4d33-b0f0-1e243473179f", "AQAAAAEAACcQAAAAEFq1x4gPnwdheYS9HqbEq+8RHcllxEdesN6YEfTFIitYAlzEr+Kv8ckBy9WW37LqYw==", "967f5fc2-73c3-4522-9da3-6e95b500c972" });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EnrolLineId",
                table: "Ticket",
                column: "EnrolLineId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolLine_EnrolLineId",
                table: "EnrolLine",
                column: "EnrolLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolLine_EnrolLine_EnrolLineId",
                table: "EnrolLine",
                column: "EnrolLineId",
                principalTable: "EnrolLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_EnrolLine_EnrolLineId",
                table: "Ticket",
                column: "EnrolLineId",
                principalTable: "EnrolLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolLine_EnrolLine_EnrolLineId",
                table: "EnrolLine");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_EnrolLine_EnrolLineId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_EnrolLineId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_EnrolLine_EnrolLineId",
                table: "EnrolLine");

            migrationBuilder.DropColumn(
                name: "EnrolLineId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "EnrolLineId",
                table: "EnrolLine");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f7e14a8-496d-477d-b504-d4c7eec1cfe2", "AQAAAAEAACcQAAAAEMGGKgSQI7aZl4YmMzb7aQU2rCxTvlIQqTJHePssXL7SHmquZDcohU/1twphmBoi0A==", "86c0259b-464e-4430-9d18-8b22df2130fa" });
        }
    }
}
