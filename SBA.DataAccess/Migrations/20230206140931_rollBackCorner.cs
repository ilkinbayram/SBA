using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class rollBackCorner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Corner_8_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Corner_9_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "IsCornerFound",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_FT_Win1",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_FT_Win2",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_FT_X",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_HT_Win1",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_HT_Win2",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_HT_X",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_SH_Win1",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_SH_Win2",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_SH_X",
                table: "FilterResults");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Corner_8_5_Over",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Corner_9_5_Over",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCornerFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_FT_Win1",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_FT_Win2",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_FT_X",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_HT_Win1",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_HT_Win2",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_HT_X",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_SH_Win1",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_SH_Win2",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_SH_X",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
