using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class Bytee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "DataFiles",
                table: "CourseContent",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "270810b9-55ed-4056-953c-903e85ffb325", "AQAAAAEAACcQAAAAEPjvdfxzktpBI3/aNHqQ9GG7ocQAmC5aHN7rlWJ3uynhPtne8viEpha9L/p9NQ6rNw==", "b12364d0-be29-4781-8f32-e6c2c322c24e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFiles",
                table: "CourseContent");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e39d6c83-6cdf-4ea1-9564-33153381d417", "AQAAAAEAACcQAAAAEFhpdUimArmEQSMpH3swgHsh/37bjUTURN/dXPkFRKTLtnsqDwXBmK6FG4h7JEtydA==", "8720024c-87d0-4543-91a2-d519dcbd6f76" });
        }
    }
}
