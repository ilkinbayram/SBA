using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class GKSavesDecimalUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(5010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(8383));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(4904),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(8213));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(4702),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(9312));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(4536),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(9157));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(2376),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(6165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(2309),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(6108));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(943),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(4885));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(826),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(4821));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 726, DateTimeKind.Local).AddTicks(2613),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(7141));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 726, DateTimeKind.Local).AddTicks(2547),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(3368));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 722, DateTimeKind.Local).AddTicks(9968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(3216));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(7877),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(2154));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(7784),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(2077));

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Shut_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Shut_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_ShutOnTarget_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_ShutOnTarget_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_GK_Saves_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_GK_Saves_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 726, DateTimeKind.Local).AddTicks(1771),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(6370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 726, DateTimeKind.Local).AddTicks(1708),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(6288));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(8383),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(8213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(4904));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(9312),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(4702));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(9157),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(4536));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(6165),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(2376));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(6108),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(2309));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(4885),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(943));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(4821),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 724, DateTimeKind.Local).AddTicks(826));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(7141),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 726, DateTimeKind.Local).AddTicks(2613));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(7083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 726, DateTimeKind.Local).AddTicks(2547));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(3368),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 268, DateTimeKind.Local).AddTicks(3216),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 722, DateTimeKind.Local).AddTicks(9968));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(2154),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(7877));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 269, DateTimeKind.Local).AddTicks(2077),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 723, DateTimeKind.Local).AddTicks(7784));

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Shut_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Shut_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_ShutOnTarget_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_ShutOnTarget_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_GK_Saves_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_GK_Saves_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(6370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 726, DateTimeKind.Local).AddTicks(1771));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 20, 35, 28, 271, DateTimeKind.Local).AddTicks(6288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 20, 39, 57, 726, DateTimeKind.Local).AddTicks(1708));
        }
    }
}
