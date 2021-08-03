using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class subtutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "645f704f-5c22-4c2d-bd30-d1f69c3fbd51", "AQAAAAEAACcQAAAAELJ3r9cqdVuGrw2PEVH6nlXFag5Gi+gEHx9cVXmUrXpuYpV5LMIvV2BeOoA3dgN1sQ==", "2c11b69c-273d-4ccb-9558-3eb17fae87e8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e256b7f-b727-4ff6-97be-bfdc7586611e", "AQAAAAEAACcQAAAAEO68odF432h31AmI/7vFpXqedMZBuKlPCYlABbfudyd94WnfUjPp7iOegMZA8NE4ZA==", "e4e0e17d-8bc3-4fbc-ae82-fb8e673319c2" });
        }
    }
}
