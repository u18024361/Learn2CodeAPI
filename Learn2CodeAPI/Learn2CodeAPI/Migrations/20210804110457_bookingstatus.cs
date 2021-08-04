using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class bookingstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bookingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingInstance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionTimeId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    TutorSessionId = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttendanceTaken = table.Column<bool>(type: "bit", nullable: false),
                    ContentUploaded = table.Column<bool>(type: "bit", nullable: false),
                    BookingStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingInstance_BookingStatus_BookingStatusId",
                        column: x => x.BookingStatusId,
                        principalTable: "BookingStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingInstance_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingInstance_SessionTime_SessionTimeId",
                        column: x => x.SessionTimeId,
                        principalTable: "SessionTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingInstance_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingInstance_TutorSession_TutorSessionId",
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
                values: new object[] { "46d748b2-c0f6-4a64-add2-48af2403ebc2", "AQAAAAEAACcQAAAAEG/pvOXmzANCuw975wPcAEj7Mp+sKdxrQag8Cn2OEp5xG96zQdfV3D+JPe+Rt6HdIg==", "d003de5d-ebab-4f05-bf57-6feb6f17bae3" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingInstance_BookingStatusId",
                table: "BookingInstance",
                column: "BookingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingInstance_ModuleId",
                table: "BookingInstance",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingInstance_SessionTimeId",
                table: "BookingInstance",
                column: "SessionTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingInstance_TutorId",
                table: "BookingInstance",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingInstance_TutorSessionId",
                table: "BookingInstance",
                column: "TutorSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingInstance");

            migrationBuilder.DropTable(
                name: "BookingStatus");

            migrationBuilder.DropTable(
                name: "SessionTime");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d50ad6af-b793-402d-8c01-44245520e226", "AQAAAAEAACcQAAAAEFI0PIf/gyC8sXXcmwj4FgnBx4cREo0+OF7Om+ykxY7RUVBtz0qzvvW+taT2LPqmFg==", "4f50d809-abf6-4cf1-b919-1976c0a6ae15" });
        }
    }
}
