using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class ResourceCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCategory", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d854fe2f-b36b-40eb-8daa-5728f1191ad7", "AQAAAAEAACcQAAAAEPfEY1y+ZEQar77J/RoefHCf7bxIFpDGkataRKUt/KCIyZG9q55gtzRlMZ065Rp2vw==", "91f5b1c6-fa22-40bb-a50e-cb0a5e1f51d3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceCategory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "645f704f-5c22-4c2d-bd30-d1f69c3fbd51", "AQAAAAEAACcQAAAAELJ3r9cqdVuGrw2PEVH6nlXFag5Gi+gEHx9cVXmUrXpuYpV5LMIvV2BeOoA3dgN1sQ==", "2c11b69c-273d-4ccb-9558-3eb17fae87e8" });
        }
    }
}
