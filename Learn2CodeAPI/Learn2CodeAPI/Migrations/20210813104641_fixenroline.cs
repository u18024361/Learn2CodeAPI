using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class fixenroline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolLine_EnrolLine_EnrolLineId",
                table: "EnrolLine");

            migrationBuilder.DropIndex(
                name: "IX_EnrolLine_EnrolLineId",
                table: "EnrolLine");

            migrationBuilder.DropColumn(
                name: "EnrolLineId",
                table: "EnrolLine");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6b5d527-96d8-4095-9f23-2c8d6b444782", "AQAAAAEAACcQAAAAEPHtrTZOiFfXAnLo42NThSdQFmsnLzlBrTkdw42BByRfdO3KkbSGpHPCvWdvQ3ENzg==", "8a349046-051b-48ec-b6d3-573df23fcac8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrolLineId",
                table: "EnrolLine",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "858448dc-1524-4d33-b0f0-1e243473179f", "AQAAAAEAACcQAAAAEFq1x4gPnwdheYS9HqbEq+8RHcllxEdesN6YEfTFIitYAlzEr+Kv8ckBy9WW37LqYw==", "967f5fc2-73c3-4522-9da3-6e95b500c972" });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolLine_EnrolLineId",
                table: "EnrolLine",
                column: "EnrolLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolLine_EnrolLine_EnrolLineId",
                table: "EnrolLine",
                column: "EnrolLineId",
                principalTable: "EnrolLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
