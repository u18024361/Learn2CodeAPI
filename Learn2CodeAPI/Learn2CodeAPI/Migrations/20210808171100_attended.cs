using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class attended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attended",
                table: "RegisteredStudent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89ee56cd-3f6f-4e6e-8526-828162cfdeb6", "AQAAAAEAACcQAAAAEGNuYz718vVLsH2kRMNVISuLNc0tmxlnNY8FSWQqe0bI1gnBk0O7VxYBUlyEiouTQQ==", "945a7d5b-9608-4c91-a09e-7d2727956f05" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attended",
                table: "RegisteredStudent");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9397d547-44b5-4efe-9934-038513f404f9", "AQAAAAEAACcQAAAAEEaTKNajW8bgvvPeD8rvRR5Ch48ou94gGzT627XcT7HlXQVKX/oLTCU6QqR673Ce0w==", "f9f51c57-4e73-42e2-abc0-ffc73c3c1900" });
        }
    }
}
