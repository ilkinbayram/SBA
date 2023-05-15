using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class AverageUpdatedWithGKSaves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(8383),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 789, DateTimeKind.Local).AddTicks(116));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(8213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 788, DateTimeKind.Local).AddTicks(9593));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(9312),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(9232));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(9157),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(9026));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(6165),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(5813));

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(6108),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(5616));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(4885),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(2294));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(4821),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(2160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(7141),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 794, DateTimeKind.Local).AddTicks(2170));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(7083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 794, DateTimeKind.Local).AddTicks(1992));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(3368),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 787, DateTimeKind.Local).AddTicks(8846));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(3216),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 787, DateTimeKind.Local).AddTicks(8191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(2154),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 789, DateTimeKind.Local).AddTicks(7031));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(2077),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 789, DateTimeKind.Local).AddTicks(6611));

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_GK_Saves_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_GK_Saves_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(6370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 793, DateTimeKind.Local).AddTicks(9887));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(6288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 793, DateTimeKind.Local).AddTicks(9754));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Average_FT_GK_Saves_AwayTeam",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_GK_Saves_HomeTeam",
                table: "AverageStatisticsHolders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 789, DateTimeKind.Local).AddTicks(116),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(8383));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 788, DateTimeKind.Local).AddTicks(9593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(8213));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(9232),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(9312));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(9026),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(9157));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(5813),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(6165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(5616),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(6108));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(2294),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(4885));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 790, DateTimeKind.Local).AddTicks(2160),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(4821));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 794, DateTimeKind.Local).AddTicks(2170),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(7141));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 794, DateTimeKind.Local).AddTicks(1992),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 787, DateTimeKind.Local).AddTicks(8846),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(3368));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 787, DateTimeKind.Local).AddTicks(8191),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(3216));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 789, DateTimeKind.Local).AddTicks(7031),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(2154));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 789, DateTimeKind.Local).AddTicks(6611),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(2077));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 793, DateTimeKind.Local).AddTicks(9887),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(6370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 14, 17, 41, 5, 793, DateTimeKind.Local).AddTicks(9754),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(6288));
        }
    }
}
