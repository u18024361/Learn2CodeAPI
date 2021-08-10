using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    TutorSessionId = table.Column<int>(type: "int", nullable: false),
                    TicketStatusId = table.Column<int>(type: "int", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Enrollment_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketStatus_TicketStatusId",
                        column: x => x.TicketStatusId,
                        principalTable: "TicketStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_TutorSession_TutorSessionId",
                        column: x => x.TutorSessionId,
                        principalTable: "TutorSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88243897-75be-4739-8ff9-63797a0d79cb", "AQAAAAEAACcQAAAAEFNC8vhbN0XOYKOOFE7l8HqYdDXY7Xp9qYbanb7iIN79LWA56YkjTfdStR0hETKAUQ==", "5707317a-e1cb-46e4-8841-40201ae048dd" });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EnrollmentId",
                table: "Ticket",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SubscriptionId",
                table: "Ticket",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketStatusId",
                table: "Ticket",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TutorSessionId",
                table: "Ticket",
                column: "TutorSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketStatus");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a02460e-7cee-483f-8ef5-4e4ee9b2fb1a", "AQAAAAEAACcQAAAAEPRe6APU6dZCLvqWXBzj3NG/ZGnMFB4pVlA9+XlFUeN52QYx9uUpK5JMdPfBdH54JQ==", "cd018618-7c81-475d-bcac-fc2365673407" });
        }
    }
}
