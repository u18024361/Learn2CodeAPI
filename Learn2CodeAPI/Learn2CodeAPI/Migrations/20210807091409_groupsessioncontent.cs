using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class groupsessioncontent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupSessionContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Recording = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SessionContentCategoryId = table.Column<int>(type: "int", nullable: false),
                    BookingInstanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSessionContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupSessionContent_BookingInstance_BookingInstanceId",
                        column: x => x.BookingInstanceId,
                        principalTable: "BookingInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupSessionContent_SessionContentCategory_SessionContentCategoryId",
                        column: x => x.SessionContentCategoryId,
                        principalTable: "SessionContentCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21c4d925-bed3-4fbe-8e11-e8fc74c5b08c", "AQAAAAEAACcQAAAAEOIj8E3TYBenbFfDuN4KykWkLNJFHjq6srtPWdaj1UDnS7MDTReSrxb6WTsrXtpGZg==", "4b7bd439-6495-4e21-b4da-f2be3bfb9f8f" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupSessionContent_BookingInstanceId",
                table: "GroupSessionContent",
                column: "BookingInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSessionContent_SessionContentCategoryId",
                table: "GroupSessionContent",
                column: "SessionContentCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupSessionContent");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81b499b6-4a42-428a-9f94-5f5f1533de35", "AQAAAAEAACcQAAAAEP3nEOAki50JPG0aINt9FPNP2xz54QhvpymZU+C7Fediy4onQyYtJhWyYXmtqlIQVQ==", "32a35536-554d-433c-a5ff-e46618e3bd62" });
        }
    }
}
