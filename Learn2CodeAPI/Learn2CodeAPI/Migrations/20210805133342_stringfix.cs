using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class stringfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResourceDescription",
                table: "Resource",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48fd51dc-a930-4390-900d-47e6879bc62e", "AQAAAAEAACcQAAAAEGjdMjMHTB+mvA7XQeO5qBl36F8ciP6BY4rYtNQRoLXJWo5EcoHt3JfVUePgjHZyjQ==", "85a78c9e-3339-47d4-ac2d-42b4bf282bed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ResourceDescription",
                table: "Resource",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "907ecce5-7f1c-40c8-bab3-528ffb360f09", "AQAAAAEAACcQAAAAEBTg2XFKf4wcZRN7OlaT0ej/rGKuM9NiENjQYfrVK0I0VaWkBY6RMWjWpGwvdwlJ/A==", "b7e85c9b-78b5-4d3d-a69f-e7262aae8a9d" });
        }
    }
}
