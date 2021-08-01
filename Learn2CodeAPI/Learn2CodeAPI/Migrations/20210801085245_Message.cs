using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class Message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba43595e-4769-4412-bead-b73e4e57ef51", "AQAAAAEAACcQAAAAEHsHacege3zZKk12iD2veQVeJvsKaDWr6V6JE7sRRN3RwETAizImMGn0UaUuLenZ7Q==", "843cc9c7-6618-4b47-8e09-03a489f424c5" });

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21d6f6ef-9679-4d56-82ad-ae041fc8399a", "AQAAAAEAACcQAAAAEKMGplNYACUgkwwsptdJQ7clHSCpINf/x6ugLd8cgTa0f1OP5ksBjo5iaheUvHEbAw==", "2b6fddcc-242d-492c-bd1c-04e6791ab314" });
        }
    }
}
