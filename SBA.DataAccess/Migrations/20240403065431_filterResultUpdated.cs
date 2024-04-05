using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class filterResultUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 705, DateTimeKind.Local).AddTicks(8631),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 702, DateTimeKind.Local).AddTicks(8677));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 705, DateTimeKind.Local).AddTicks(8479),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 702, DateTimeKind.Local).AddTicks(8541));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(9180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(6655));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(5009),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(3933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(4828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(3797));

            migrationBuilder.AddColumn<int>(
                name: "AwayFtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "AwayHtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "AwayShGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "HomeFtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "HomeHtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "HomeShGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayFtGoalCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "AwayHtGoalCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "AwayShGoalCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "HomeFtGoalCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "HomeHtGoalCount",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "HomeShGoalCount",
                table: "FilterResults");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 702, DateTimeKind.Local).AddTicks(8677),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 705, DateTimeKind.Local).AddTicks(8631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 702, DateTimeKind.Local).AddTicks(8541),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 705, DateTimeKind.Local).AddTicks(8479));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(6655),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(9180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(3933),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(5009));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(3797),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 10, 54, 31, 706, DateTimeKind.Local).AddTicks(4828));
        }
    }
}
