using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class stringpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentDate",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentAmount",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81b499b6-4a42-428a-9f94-5f5f1533de35", "AQAAAAEAACcQAAAAEP3nEOAki50JPG0aINt9FPNP2xz54QhvpymZU+C7Fediy4onQyYtJhWyYXmtqlIQVQ==", "32a35536-554d-433c-a5ff-e46618e3bd62" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PaymentAmount",
                table: "Payment",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bfc65fc5-7f87-42a2-89a1-fc78756cc358", "AQAAAAEAACcQAAAAEMQYyM3HvfbSmEZQXKw37rz0tvvGt1Eu1nKpmJUUXBkS7bmP2EwU6VSB2PEd0SIsnw==", "e0fb287b-5859-4a2b-921d-970d193270ec" });
        }
    }
}
