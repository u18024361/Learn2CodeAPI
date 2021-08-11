using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class coursecontentname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Filepath",
                table: "CourseContent",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "DataFiles",
                table: "CourseContent",
                newName: "Content");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61c82439-ea94-4d77-8c36-4c8c7cad2f4e", "AQAAAAEAACcQAAAAEM7nYi1BJaxnjTPg4zjFTS1I9LhiMg3HmY8tVAm8tmECqIBY19yFfzotg/ZrGNuByQ==", "2acf56e1-91bb-41a9-a0e1-6c4857e8aff5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "CourseContent",
                newName: "Filepath");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "CourseContent",
                newName: "DataFiles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40c27b91-9383-4193-85a2-5019d89d3df0", "AQAAAAEAACcQAAAAENzlHxQB8Z5NZFKR/2myqJu/9cABy0fFHyglSsOOEjyAvBEqbo5uT8UkeNiPWKgIHQ==", "b359254d-df29-4cb1-a4e8-7037eb6703ac" });
        }
    }
}
