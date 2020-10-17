using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MapImage",
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
                    table.PrimaryKey("PK_MapImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.Id);
                    table.ForeignKey(
                        name: "FK1",
                        column: x => x.ImageId,
                        principalTable: "MapImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Map_ImageId",
                table: "Map",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Map");

            migrationBuilder.DropTable(
                name: "MapImage");
        }
    }
}
