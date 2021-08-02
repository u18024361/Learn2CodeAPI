using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class Coursecontenttwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ca836d3-53e1-4669-acf5-bde2690092b3", "AQAAAAEAACcQAAAAEBPlptz73t1n/fVo8IYDFmVe/vuuH5UCH3lJbj4dpUvWNKX+j/ap5QBN/nD91n61Ug==", "51e534d8-4bd5-48c6-af72-b53459ce8174" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "270810b9-55ed-4056-953c-903e85ffb325", "AQAAAAEAACcQAAAAEPjvdfxzktpBI3/aNHqQ9GG7ocQAmC5aHN7rlWJ3uynhPtne8viEpha9L/p9NQ6rNw==", "b12364d0-be29-4781-8f32-e6c2c322c24e" });
        }
    }
}
