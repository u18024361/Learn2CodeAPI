using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class cs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39be417b-4dde-4773-aead-e5f438bf3741", "AQAAAAEAACcQAAAAEGp5c4joYnH4U+XOTJoqrFDR2+VjIsNwFWKFlI+EQ5m9YtVmvXDrWwh4Nl4jXNaT4g==", "8b9ecd93-d3ba-4578-a592-735338ce50aa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85256774-6067-475e-b828-98b81c587c75", "AQAAAAEAACcQAAAAEOd8BhFMfqL1bxYl4QF92cT06fgHaBzw5D7rquRdM49Ph0clAU/ENUNKJP6B3yDpJw==", "cd20a11a-64c2-4db7-8908-cf44af598578" });
        }
    }
}
