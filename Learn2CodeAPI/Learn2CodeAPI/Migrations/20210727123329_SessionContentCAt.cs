using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class SessionContentCAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionContentCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionContentCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionContentCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionContentCategory_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionContentCategory_AdminId",
                table: "SessionContentCategory",
                column: "AdminId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionContentCategory");
        }
    }
}
