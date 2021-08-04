using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class fkstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInstance_BookingStatus_BookingStatusId",
                table: "BookingInstance");

            migrationBuilder.AlterColumn<int>(
                name: "BookingStatusId",
                table: "BookingInstance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99b935ec-8edb-46a4-9ca8-3374810ebe51", "AQAAAAEAACcQAAAAEHwjiqvIVa65zTDHoaIhcSpXB2NLXgudv0EHc4b3Yov+B6lY1MYzqdUbyILBLWU1sw==", "f04d0fef-e771-4747-a9d7-c73ce76a328e" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInstance_BookingStatus_BookingStatusId",
                table: "BookingInstance",
                column: "BookingStatusId",
                principalTable: "BookingStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInstance_BookingStatus_BookingStatusId",
                table: "BookingInstance");

            migrationBuilder.AlterColumn<int>(
                name: "BookingStatusId",
                table: "BookingInstance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46d748b2-c0f6-4a64-add2-48af2403ebc2", "AQAAAAEAACcQAAAAEG/pvOXmzANCuw975wPcAEj7Mp+sKdxrQag8Cn2OEp5xG96zQdfV3D+JPe+Rt6HdIg==", "d003de5d-ebab-4f05-bf57-6feb6f17bae3" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInstance_BookingStatus_BookingStatusId",
                table: "BookingInstance",
                column: "BookingStatusId",
                principalTable: "BookingStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
