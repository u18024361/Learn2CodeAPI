using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class TutorSessionModuleTutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TutorSessionModuleTutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    TutorSessionId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSessionModuleTutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorSessionModuleTutor_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSessionModuleTutor_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSessionModuleTutor_TutorSession_TutorSessionId",
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
                values: new object[] { "414fec01-cd8a-405a-a260-d42d5cce8c3e", "AQAAAAEAACcQAAAAELAodT1yXBvCNYmOimFLiiK4ykYF7DKEt3S7xMwdYmp+12veQKOPAWzL4Mq3DKIu/w==", "c5bd52bf-1f07-4c41-8e0d-b834719c4498" });

            migrationBuilder.CreateIndex(
                name: "IX_TutorSessionModuleTutor_ModuleId",
                table: "TutorSessionModuleTutor",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSessionModuleTutor_TutorId",
                table: "TutorSessionModuleTutor",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSessionModuleTutor_TutorSessionId",
                table: "TutorSessionModuleTutor",
                column: "TutorSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorSessionModuleTutor");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee75a143-1590-4908-b02f-dd31134b2d77", "AQAAAAEAACcQAAAAECjp+TD2sPxZ+/3MlvMpHgBOG1WohwLpR65PuWCBxZyox3nZrDKqcE2NJuG0TEcrPA==", "64b68f03-bfea-4291-abff-e16567f3c165" });
        }
    }
}
