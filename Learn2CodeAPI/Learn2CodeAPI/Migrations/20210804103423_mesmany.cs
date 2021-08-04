using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class mesmany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d50ad6af-b793-402d-8c01-44245520e226", "AQAAAAEAACcQAAAAEFI0PIf/gyC8sXXcmwj4FgnBx4cREo0+OF7Om+ykxY7RUVBtz0qzvvW+taT2LPqmFg==", "4f50d809-abf6-4cf1-b919-1976c0a6ae15" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14de3b10-bd5f-4695-aca7-315c5b04d8a8", "AQAAAAEAACcQAAAAEP+QsxOQ0LRHt3oiXb/ms8b/RkB+8B1GcrXCSFYyA/Xp4lR3i3BzKLnI7IGyX+PDKg==", "6f509042-329f-49d0-8fe3-8ded7fdaaba4" });
        }
    }
}
