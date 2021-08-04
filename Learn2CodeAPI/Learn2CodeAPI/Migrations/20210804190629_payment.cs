using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fc85e60-0360-4f4d-a7b1-5d37695b18ba", "AQAAAAEAACcQAAAAEKzRtMQ925FV48KBuwLBO5OmwEgXuEk6MdUpxCBp1ZEBsgRUx4FV2ZgoG4sRTqfjQw==", "a4d7db80-9c25-45b4-bae4-778117aa6de7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "692c1ddc-faf7-4a1f-ad6e-8744e19bfda7", "AQAAAAEAACcQAAAAEIOwjS1gclNqJ/QP0btqbDFa7FFnGSuLxPFMPV/k30Yn8OhG0HtjgYFsX6A5KdFCWQ==", "20e784b8-68dd-4e43-a740-acd908a328ae" });
        }
    }
}
