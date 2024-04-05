using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class xResultUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Is_SH_X",
                table: "PerformanceOverall",
                newName: "Is_SH_X2");

            migrationBuilder.RenameColumn(
                name: "Is_HT_X",
                table: "PerformanceOverall",
                newName: "Is_SH_X1");

            migrationBuilder.RenameColumn(
                name: "Is_FT_X",
                table: "PerformanceOverall",
                newName: "Is_HT_X2");

            migrationBuilder.AddColumn<int>(
                name: "Is_FT_X1",
                table: "PerformanceOverall",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Is_FT_X2",
                table: "PerformanceOverall",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Is_HT_X1",
                table: "PerformanceOverall",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 551, DateTimeKind.Local).AddTicks(6019),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 705, DateTimeKind.Local).AddTicks(8631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 551, DateTimeKind.Local).AddTicks(5861),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 705, DateTimeKind.Local).AddTicks(8479));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 553, DateTimeKind.Local).AddTicks(35),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(9180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 552, DateTimeKind.Local).AddTicks(4979),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(5009));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 552, DateTimeKind.Local).AddTicks(4609),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(4828));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_FT_X1",
                table: "PerformanceOverall");

            migrationBuilder.DropColumn(
                name: "Is_FT_X2",
                table: "PerformanceOverall");

            migrationBuilder.DropColumn(
                name: "Is_HT_X1",
                table: "PerformanceOverall");

            migrationBuilder.RenameColumn(
                name: "Is_SH_X2",
                table: "PerformanceOverall",
                newName: "Is_SH_X");

            migrationBuilder.RenameColumn(
                name: "Is_SH_X1",
                table: "PerformanceOverall",
                newName: "Is_HT_X");

            migrationBuilder.RenameColumn(
                name: "Is_HT_X2",
                table: "PerformanceOverall",
                newName: "Is_FT_X");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 705, DateTimeKind.Local).AddTicks(8631),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 551, DateTimeKind.Local).AddTicks(6019));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 705, DateTimeKind.Local).AddTicks(8479),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 551, DateTimeKind.Local).AddTicks(5861));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(9180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 553, DateTimeKind.Local).AddTicks(35));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(5009),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 552, DateTimeKind.Local).AddTicks(4979));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(4828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 552, DateTimeKind.Local).AddTicks(4609));
        }
    }
}
