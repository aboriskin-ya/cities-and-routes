using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AlterSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FoundPathColor",
                table: "Settings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "FoundPathSize",
                table: "Settings",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoundPathColor",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FoundPathSize",
                table: "Settings");
        }
    }
}
