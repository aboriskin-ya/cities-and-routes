using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class CityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    MapId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK2",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_MapId",
                table: "City",
                column: "MapId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
