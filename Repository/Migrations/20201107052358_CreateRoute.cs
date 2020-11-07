using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class CreateRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MapId1",
                table: "Settings",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Y",
                table: "City",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "X",
                table: "City",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "MapId1",
                table: "City",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    Distance = table.Column<int>(nullable: false),
                    FirstCityId = table.Column<Guid>(nullable: false),
                    SecondCityId = table.Column<Guid>(nullable: false),
                    MapId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Route_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_MapId1",
                table: "Settings",
                column: "MapId1",
                unique: true,
                filter: "[MapId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_City_MapId1",
                table: "City",
                column: "MapId1");

            migrationBuilder.CreateIndex(
                name: "IX_Route_MapId",
                table: "Route",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Map_MapId1",
                table: "City",
                column: "MapId1",
                principalTable: "Map",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Map_MapId1",
                table: "Settings",
                column: "MapId1",
                principalTable: "Map",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Map_MapId1",
                table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Map_MapId1",
                table: "Settings");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropIndex(
                name: "IX_Settings_MapId1",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_City_MapId1",
                table: "City");

            migrationBuilder.DropColumn(
                name: "MapId1",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "MapId1",
                table: "City");

            migrationBuilder.AlterColumn<int>(
                name: "Y",
                table: "City",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "X",
                table: "City",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
