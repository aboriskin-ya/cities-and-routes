using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
<<<<<<< HEAD:Repository/Migrations/20201118110050_NewMigration.cs
    public partial class NewMigration : Migration
=======
    public partial class CreateNewDB : Migration
>>>>>>> develop:Repository/Migrations/20201118203106_CreateNewDB.cs
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    Data = table.Column<byte[]>(type: "image", nullable: false),
                    ContentType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Map_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    X = table.Column<double>(nullable: false),
                    Y = table.Column<double>(nullable: false),
<<<<<<< HEAD:Repository/Migrations/20201118110050_NewMigration.cs
                    MapId = table.Column<Guid>(nullable: false),
                    RouteId = table.Column<Guid>(nullable: false)
=======
                    MapId = table.Column<Guid>(nullable: false)
>>>>>>> develop:Repository/Migrations/20201118203106_CreateNewDB.cs
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    DisplayingImage = table.Column<bool>(nullable: false),
                    DisplayingGraph = table.Column<bool>(nullable: false),
                    VertexSize = table.Column<double>(nullable: false),
                    VertexColor = table.Column<string>(nullable: false),
                    EdgeSize = table.Column<double>(nullable: false),
                    EdgeColor = table.Column<string>(nullable: false),
                    MapId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
<<<<<<< HEAD:Repository/Migrations/20201118110050_NewMigration.cs
                        name: "FK_Route_City_FirstCityId1",
=======
                        name: "FK_Route_City_FirstCityId",
>>>>>>> develop:Repository/Migrations/20201118203106_CreateNewDB.cs
                        column: x => x.FirstCityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Route_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
<<<<<<< HEAD:Repository/Migrations/20201118110050_NewMigration.cs
                        name: "FK_Route_City_SecondCityId1",
=======
                        name: "FK_Route_City_SecondCityId",
>>>>>>> develop:Repository/Migrations/20201118203106_CreateNewDB.cs
                        column: x => x.SecondCityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
<<<<<<< HEAD:Repository/Migrations/20201118110050_NewMigration.cs
=======

            migrationBuilder.CreateIndex(
                name: "IX_City_MapId",
                table: "City",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Map_ImageId",
                table: "Map",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Route_FirstCityId",
                table: "Route",
                column: "FirstCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_MapId",
                table: "Route",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_SecondCityId",
                table: "Route",
                column: "SecondCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_MapId",
                table: "Settings",
                column: "MapId",
                unique: true);
>>>>>>> develop:Repository/Migrations/20201118203106_CreateNewDB.cs
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Map");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
