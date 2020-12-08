using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AlterTableSettings : Migration
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
                migrationBuilder.AddColumn<bool>(
                    name: "DisplayCitiesNames",
                    table: "Settings",
                    nullable: false,
                    defaultValue: true);
         }

         protected override void Down(MigrationBuilder migrationBuilder)
         {
                migrationBuilder.DropColumn(
                    name: "FoundPathColor",
                    table: "Settings");

                migrationBuilder.DropColumn(
                    name: "FoundPathSize",
                    table: "Settings");

                migrationBuilder.DropColumn(
                    name: "DisplayCitiesNames",
                    table: "Settings");
        }
    }
}
