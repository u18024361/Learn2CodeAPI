using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn2CodeAPI.Migrations
{
    public partial class fixx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescriptionName",
                table: "Subscription",
                newName: "SubscriptionName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e256b7f-b727-4ff6-97be-bfdc7586611e", "AQAAAAEAACcQAAAAEO68odF432h31AmI/7vFpXqedMZBuKlPCYlABbfudyd94WnfUjPp7iOegMZA8NE4ZA==", "e4e0e17d-8bc3-4fbc-ae82-fb8e673319c2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionName",
                table: "Subscription",
                newName: "DescriptionName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7591b0c-0184-4c0f-b32a-656d0f531a18", "AQAAAAEAACcQAAAAEBd3+v7WAvIisILbuzJE3qolySBtXxzxyY1LfUbaU2DXh3hlVsDQlfCscyW3qHD3/g==", "69cadb07-871b-4805-acfa-ea0a996661fb" });
        }
    }
}
