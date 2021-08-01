using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05889596-d48e-43fa-a19c-f70a18df4814", "AQAAAAEAACcQAAAAEFa/JvJp2EXM3bc3Zt6mzPJq0Ji7PnP8pzL8dSBU2tFGe2mimwLQQ/DBqYHpnIpeow==", "6d06dc6a-2606-4a8c-bde0-f746b5cc876c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba43595e-4769-4412-bead-b73e4e57ef51", "AQAAAAEAACcQAAAAEHsHacege3zZKk12iD2veQVeJvsKaDWr6V6JE7sRRN3RwETAizImMGn0UaUuLenZ7Q==", "843cc9c7-6618-4b47-8e09-03a489f424c5" });
        }
    }
}
