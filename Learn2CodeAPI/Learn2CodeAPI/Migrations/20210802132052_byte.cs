using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class @byte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e39d6c83-6cdf-4ea1-9564-33153381d417", "AQAAAAEAACcQAAAAEFhpdUimArmEQSMpH3swgHsh/37bjUTURN/dXPkFRKTLtnsqDwXBmK6FG4h7JEtydA==", "8720024c-87d0-4543-91a2-d519dcbd6f76" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bef04d7-8f20-4625-8da1-1155f6642f7b", "AQAAAAEAACcQAAAAEDIXgQWswJ4c21O++4kQ1wicdngeXmw8MD82WzqnADnn6vW+8K9clZNvpGWZ/hE9/A==", "9b2d06b8-90a7-4534-826a-a991c22229a6" });
        }
    }
}
