using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class CSvv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payment",
                newName: "FullName");

            migrationBuilder.AddColumn<double>(
                name: "PaymentAmount",
                table: "Payment",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bfc65fc5-7f87-42a2-89a1-fc78756cc358", "AQAAAAEAACcQAAAAEMQYyM3HvfbSmEZQXKw37rz0tvvGt1Eu1nKpmJUUXBkS7bmP2EwU6VSB2PEd0SIsnw==", "e0fb287b-5859-4a2b-921d-970d193270ec" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Payment",
                newName: "Amount");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61c82439-ea94-4d77-8c36-4c8c7cad2f4e", "AQAAAAEAACcQAAAAEM7nYi1BJaxnjTPg4zjFTS1I9LhiMg3HmY8tVAm8tmECqIBY19yFfzotg/ZrGNuByQ==", "2acf56e1-91bb-41a9-a0e1-6c4857e8aff5" });
        }
    }
}
