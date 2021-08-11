using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class bytearray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "TutorPhoto",
                table: "Tutor",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileName",
                table: "File",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40c27b91-9383-4193-85a2-5019d89d3df0", "AQAAAAEAACcQAAAAENzlHxQB8Z5NZFKR/2myqJu/9cABy0fFHyglSsOOEjyAvBEqbo5uT8UkeNiPWKgIHQ==", "b359254d-df29-4cb1-a4e8-7037eb6703ac" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TutorPhoto",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "File");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7710d9d9-65fa-49c7-9fe8-e35ee73843c9", "AQAAAAEAACcQAAAAEPvBGSCPDhVFYL5+TwmN2/ATUn1+OBGv1qTWR4iqORyAjNsDzAuMpaAKFyDrj6V0MA==", "37b1fd62-145c-4de4-8073-08b283865713" });
        }
    }
}
