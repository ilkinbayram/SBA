using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class FilterResultUpdatedWithPossesionShot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwayPossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AwayShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AwayShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomePossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPossesionFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShotFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShotOnTargetFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayPossesion",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "AwayShotCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "AwayShotOnTargetCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "HomePossesion",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "HomeShotCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "HomeShotOnTargetCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "IsPossesionFound",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "IsShotFound",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "IsShotOnTargetFound",
                table: "FilterResults");
        }
    }
}
