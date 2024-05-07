using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class leagueIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(3659),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(1921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(3487),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(1699));

            migrationBuilder.AddColumn<bool>(
                name: "IsCountryLeagueUpdated",
                table: "MatchBets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "MatchBets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 631, DateTimeKind.Local).AddTicks(2518),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 904, DateTimeKind.Local).AddTicks(1139));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(8822),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(7349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(8695),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(7219));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCountryLeagueUpdated",
                table: "MatchBets");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "MatchBets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(1921),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(3659));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(1699),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(3487));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 904, DateTimeKind.Local).AddTicks(1139),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 631, DateTimeKind.Local).AddTicks(2518));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(7349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(8822));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(7219),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(8695));
        }
    }
}
