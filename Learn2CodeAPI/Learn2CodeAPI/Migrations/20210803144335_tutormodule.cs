using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class tutormodule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TutorModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorModule_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorModule_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee75a143-1590-4908-b02f-dd31134b2d77", "AQAAAAEAACcQAAAAECjp+TD2sPxZ+/3MlvMpHgBOG1WohwLpR65PuWCBxZyox3nZrDKqcE2NJuG0TEcrPA==", "64b68f03-bfea-4291-abff-e16567f3c165" });

            migrationBuilder.CreateIndex(
                name: "IX_TutorModule_ModuleId",
                table: "TutorModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorModule_TutorId",
                table: "TutorModule",
                column: "TutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorModule");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d854fe2f-b36b-40eb-8daa-5728f1191ad7", "AQAAAAEAACcQAAAAEPfEY1y+ZEQar77J/RoefHCf7bxIFpDGkataRKUt/KCIyZG9q55gtzRlMZ065Rp2vw==", "91f5b1c6-fa22-40bb-a50e-cb0a5e1f51d3" });
        }
    }
}
