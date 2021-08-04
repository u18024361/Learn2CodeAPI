using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class datestring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "BookingInstance",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "692c1ddc-faf7-4a1f-ad6e-8744e19bfda7", "AQAAAAEAACcQAAAAEIOwjS1gclNqJ/QP0btqbDFa7FFnGSuLxPFMPV/k30Yn8OhG0HtjgYFsX6A5KdFCWQ==", "20e784b8-68dd-4e43-a740-acd908a328ae" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "BookingInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99b935ec-8edb-46a4-9ca8-3374810ebe51", "AQAAAAEAACcQAAAAEHwjiqvIVa65zTDHoaIhcSpXB2NLXgudv0EHc4b3Yov+B6lY1MYzqdUbyILBLWU1sw==", "f04d0fef-e771-4747-a9d7-c73ce76a328e" });
        }
    }
}
