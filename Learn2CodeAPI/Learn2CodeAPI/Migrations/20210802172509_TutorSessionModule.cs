using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class TutorSessionModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsGroup = table.Column<bool>(type: "bit", nullable: false),
                    SessionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TutorSession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorSession_SessionType_SessionTypeId",
                        column: x => x.SessionTypeId,
                        principalTable: "SessionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorSessionModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    TutorSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSessionModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorSessionModule_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSessionModule_TutorSession_TutorSessionId",
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
                values: new object[] { "13831cb7-f2de-4cac-aed6-e12c890ef9cc", "AQAAAAEAACcQAAAAEEUlNbA/ILCL/WvWdR8Kx/k/UWmU/9+h1RslhdCoTLCo4tJNLNZoJsK96ddzr+iwNQ==", "a3c2b1ae-5b88-4a6d-8152-00c36eaf1d3f" });

            migrationBuilder.CreateIndex(
                name: "IX_TutorSession_SessionTypeId",
                table: "TutorSession",
                column: "SessionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSessionModule_ModuleId",
                table: "TutorSessionModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSessionModule_TutorSessionId",
                table: "TutorSessionModule",
                column: "TutorSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorSessionModule");

            migrationBuilder.DropTable(
                name: "TutorSession");

            migrationBuilder.DropTable(
                name: "SessionType");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ca836d3-53e1-4669-acf5-bde2690092b3", "AQAAAAEAACcQAAAAEBPlptz73t1n/fVo8IYDFmVe/vuuH5UCH3lJbj4dpUvWNKX+j/ap5QBN/nD91n61Ug==", "51e534d8-4bd5-48c6-af72-b53459ce8174" });
        }
    }
}
