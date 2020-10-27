using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RouteEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    Distance = table.Column<int>(nullable: false),
                    MapId = table.Column<Guid>(nullable: false),
                    FirstCityId = table.Column<Guid>(nullable: false),
                    SecondCityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK1",
                        column: x => x.FirstCityId,
                        principalTable: "FirstCityId",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK2",
                        column: x => x.SecondCityId,
                        principalTable: "SecondCityId",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK3",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Route_MapId",
                table: "Route",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_FirstCityId",
                table: "Route",
                column: "FirstCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_SecondCityId",
                table: "Route",
                column: "SecondCityId");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Route");
        }
    }
}
