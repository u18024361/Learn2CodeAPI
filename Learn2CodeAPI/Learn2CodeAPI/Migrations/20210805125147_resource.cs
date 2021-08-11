using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class resource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResoucesName = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ResourceDescription = table.Column<int>(type: "int", nullable: false),
                    ResourceCategoryId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resource_ResourceCategory_ResourceCategoryId",
                        column: x => x.ResourceCategoryId,
                        principalTable: "ResourceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "907ecce5-7f1c-40c8-bab3-528ffb360f09", "AQAAAAEAACcQAAAAEBTg2XFKf4wcZRN7OlaT0ej/rGKuM9NiENjQYfrVK0I0VaWkBY6RMWjWpGwvdwlJ/A==", "b7e85c9b-78b5-4d3d-a69f-e7262aae8a9d" });

            migrationBuilder.CreateIndex(
                name: "IX_Resource_ModuleId",
                table: "Resource",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_ResourceCategoryId",
                table: "Resource",
                column: "ResourceCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fc85e60-0360-4f4d-a7b1-5d37695b18ba", "AQAAAAEAACcQAAAAEKzRtMQ925FV48KBuwLBO5OmwEgXuEk6MdUpxCBp1ZEBsgRUx4FV2ZgoG4sRTqfjQw==", "a4d7db80-9c25-45b4-bae4-778117aa6de7" });
        }
    }
}
