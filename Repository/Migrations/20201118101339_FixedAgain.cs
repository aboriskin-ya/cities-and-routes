using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class FixedAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Map_MapId",
                table: "City");
            migrationBuilder.DropForeignKey(
                name: "FK_City_Map_MapId1",
                table: "City");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Map_MapId",
                table: "City",
                column: "MapId",
                principalTable: "Map",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Map_MapId",
                table: "City");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Map_MapId",
                table: "City",
                column: "MapId",
                principalTable: "Map",
                principalColumn: "Id");
        }
    }
}
