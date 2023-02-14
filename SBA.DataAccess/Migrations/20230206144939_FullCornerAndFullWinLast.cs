using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class FullCornerAndFullWinLast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwayCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Corner_7_5_Over",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                name: "Corner_Away_3_5_Over",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Corner_Away_4_5_Over",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Corner_Home_3_5_Over",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Corner_Home_4_5_Over",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "HomeCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCornerFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_Corner_FT_Win1",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_Corner_FT_Win2",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_Corner_FT_X",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayCornerCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Corner_7_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Corner_8_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Corner_9_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Corner_Away_3_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Corner_Away_4_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Corner_Home_3_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Corner_Home_4_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "HomeCornerCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "IsCornerFound",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_Corner_FT_Win1",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_Corner_FT_Win2",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Is_Corner_FT_X",
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
    }
}
