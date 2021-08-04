using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class icollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14de3b10-bd5f-4695-aca7-315c5b04d8a8", "AQAAAAEAACcQAAAAEP+QsxOQ0LRHt3oiXb/ms8b/RkB+8B1GcrXCSFYyA/Xp4lR3i3BzKLnI7IGyX+PDKg==", "6f509042-329f-49d0-8fe3-8ded7fdaaba4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "414fec01-cd8a-405a-a260-d42d5cce8c3e", "AQAAAAEAACcQAAAAELAodT1yXBvCNYmOimFLiiK4ykYF7DKEt3S7xMwdYmp+12veQKOPAWzL4Mq3DKIu/w==", "c5bd52bf-1f07-4c41-8e0d-b834719c4498" });
        }
    }
}
