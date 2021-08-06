using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TutorPhoto",
                table: "Tutor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "File",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48fd51dc-a930-4390-900d-47e6879bc62e", "AQAAAAEAACcQAAAAEGjdMjMHTB+mvA7XQeO5qBl36F8ciP6BY4rYtNQRoLXJWo5EcoHt3JfVUePgjHZyjQ==", "85a78c9e-3339-47d4-ac2d-42b4bf282bed" });
        }
    }
}
