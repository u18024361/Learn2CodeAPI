using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class subshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollment_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubScriptionBasketLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubScriptionBasketLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubScriptionBasketLine_Basket_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Basket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubScriptionBasketLine_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubScriptionBasketLine_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrolLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolLine_Enrollment_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolLine_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolLine_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a02460e-7cee-483f-8ef5-4e4ee9b2fb1a", "AQAAAAEAACcQAAAAEPRe6APU6dZCLvqWXBzj3NG/ZGnMFB4pVlA9+XlFUeN52QYx9uUpK5JMdPfBdH54JQ==", "cd018618-7c81-475d-bcac-fc2365673407" });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolLine_EnrollmentId",
                table: "EnrolLine",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolLine_ModuleId",
                table: "EnrolLine",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolLine_SubscriptionId",
                table: "EnrolLine",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentId",
                table: "Enrollment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubScriptionBasketLine_BasketId",
                table: "SubScriptionBasketLine",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_SubScriptionBasketLine_ModuleId",
                table: "SubScriptionBasketLine",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubScriptionBasketLine_SubscriptionId",
                table: "SubScriptionBasketLine",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolLine");

            migrationBuilder.DropTable(
                name: "SubScriptionBasketLine");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00faab49-16b7-4b65-a92e-ee63278052f1", "AQAAAAEAACcQAAAAEABUa5gmd2XCjA++qkeUw0Er5i8SzaaGlBuhSWPwmZOJMesGRvqyXhtZvoSWfqG+Fw==", "5a6acd09-69cc-4e16-aefb-08c7e98ee14f" });
        }
    }
}
