using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class Subscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    DescriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTutorSession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    TutorSessionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTutorSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionTutorSession_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionTutorSession_TutorSession_TutorSessionId",
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
                values: new object[] { "b7591b0c-0184-4c0f-b32a-656d0f531a18", "AQAAAAEAACcQAAAAEBd3+v7WAvIisILbuzJE3qolySBtXxzxyY1LfUbaU2DXh3hlVsDQlfCscyW3qHD3/g==", "69cadb07-871b-4805-acfa-ea0a996661fb" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_AdminId",
                table: "Subscription",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTutorSession_SubscriptionId",
                table: "SubscriptionTutorSession",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTutorSession_TutorSessionId",
                table: "SubscriptionTutorSession",
                column: "TutorSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionTutorSession");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9858e05d-7a15-43aa-b3ed-5fbd9ace631f", "AQAAAAEAACcQAAAAEB0xy9ZdUXVspxfMevF3HqeaEXXBP76xIhY3aQK994KD4hhPsh9W2c1gD1AbwkHm4g==", "e1e8c0d6-fc3d-4c58-83bf-ddd1ed47babb" });
        }
    }
}
