using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class propertiesAreCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SH_GG",
                table: "AverageStatisticsHolders",
                newName: "SH_GG_Home");

            migrationBuilder.RenameColumn(
                name: "SH_15_Over",
                table: "AverageStatisticsHolders",
                newName: "SH_GG_Away");

            migrationBuilder.RenameColumn(
                name: "SH_05_Over",
                table: "AverageStatisticsHolders",
                newName: "SH_15_Over_Home");

            migrationBuilder.RenameColumn(
                name: "Is_SH_X",
                table: "AverageStatisticsHolders",
                newName: "SH_15_Over_Away");

            migrationBuilder.RenameColumn(
                name: "Is_HT_X",
                table: "AverageStatisticsHolders",
                newName: "SH_05_Over_Home");

            migrationBuilder.RenameColumn(
                name: "Is_FT_X",
                table: "AverageStatisticsHolders",
                newName: "SH_05_Over_Away");

            migrationBuilder.RenameColumn(
                name: "Is_Corner_FT_X",
                table: "AverageStatisticsHolders",
                newName: "Is_SH_X2");

            migrationBuilder.RenameColumn(
                name: "HT_GG",
                table: "AverageStatisticsHolders",
                newName: "Is_SH_X1");

            migrationBuilder.RenameColumn(
                name: "HT_15_Over",
                table: "AverageStatisticsHolders",
                newName: "Is_HT_X2");

            migrationBuilder.RenameColumn(
                name: "HT_05_Over",
                table: "AverageStatisticsHolders",
                newName: "Is_HT_X1");

            migrationBuilder.RenameColumn(
                name: "FT_GG",
                table: "AverageStatisticsHolders",
                newName: "Is_FT_X2");

            migrationBuilder.RenameColumn(
                name: "FT_35_Over",
                table: "AverageStatisticsHolders",
                newName: "Is_FT_X1");

            migrationBuilder.RenameColumn(
                name: "FT_25_Over",
                table: "AverageStatisticsHolders",
                newName: "Is_Corner_FT_X2");

            migrationBuilder.RenameColumn(
                name: "FT_15_Over",
                table: "AverageStatisticsHolders",
                newName: "Is_Corner_FT_X1");

            migrationBuilder.RenameColumn(
                name: "Corner_9_5_Over",
                table: "AverageStatisticsHolders",
                newName: "HT_GG_Home");

            migrationBuilder.RenameColumn(
                name: "Corner_8_5_Over",
                table: "AverageStatisticsHolders",
                newName: "HT_GG_Away");

            migrationBuilder.RenameColumn(
                name: "Corner_7_5_Over",
                table: "AverageStatisticsHolders",
                newName: "HT_15_Over_Home");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StatisticInfoHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PossibleForecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FT_Result",
                table: "MatchForecastsFM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HT_Result",
                table: "MatchForecastsFM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchForecastsFM",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Corner_7_5_Over_Away",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Corner_7_5_Over_Home",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Corner_8_5_Over_Away",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Corner_8_5_Over_Home",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Corner_9_5_Over_Away",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Corner_9_5_Over_Home",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FT_15_Over_Away",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FT_15_Over_Home",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FT_25_Over_Away",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FT_25_Over_Home",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FT_35_Over_Away",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FT_35_Over_Home",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FT_GG_Away",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FT_GG_Home",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HT_05_Over_Away",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HT_05_Over_Home",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HT_15_Over_Away",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FT_Result",
                table: "MatchForecastsFM");

            migrationBuilder.DropColumn(
                name: "HT_Result",
                table: "MatchForecastsFM");

            migrationBuilder.DropColumn(
                name: "MatchDateTime",
                table: "MatchForecastsFM");

            migrationBuilder.DropColumn(
                name: "Corner_7_5_Over_Away",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Corner_7_5_Over_Home",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Corner_8_5_Over_Away",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Corner_8_5_Over_Home",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Corner_9_5_Over_Away",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Corner_9_5_Over_Home",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "FT_15_Over_Away",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "FT_15_Over_Home",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "FT_25_Over_Away",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "FT_25_Over_Home",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "FT_35_Over_Away",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "FT_35_Over_Home",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "FT_GG_Away",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "FT_GG_Home",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "HT_05_Over_Away",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "HT_05_Over_Home",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "HT_15_Over_Away",
                table: "AverageStatisticsHolders");

            migrationBuilder.RenameColumn(
                name: "SH_GG_Home",
                table: "AverageStatisticsHolders",
                newName: "SH_GG");

            migrationBuilder.RenameColumn(
                name: "SH_GG_Away",
                table: "AverageStatisticsHolders",
                newName: "SH_15_Over");

            migrationBuilder.RenameColumn(
                name: "SH_15_Over_Home",
                table: "AverageStatisticsHolders",
                newName: "SH_05_Over");

            migrationBuilder.RenameColumn(
                name: "SH_15_Over_Away",
                table: "AverageStatisticsHolders",
                newName: "Is_SH_X");

            migrationBuilder.RenameColumn(
                name: "SH_05_Over_Home",
                table: "AverageStatisticsHolders",
                newName: "Is_HT_X");

            migrationBuilder.RenameColumn(
                name: "SH_05_Over_Away",
                table: "AverageStatisticsHolders",
                newName: "Is_FT_X");

            migrationBuilder.RenameColumn(
                name: "Is_SH_X2",
                table: "AverageStatisticsHolders",
                newName: "Is_Corner_FT_X");

            migrationBuilder.RenameColumn(
                name: "Is_SH_X1",
                table: "AverageStatisticsHolders",
                newName: "HT_GG");

            migrationBuilder.RenameColumn(
                name: "Is_HT_X2",
                table: "AverageStatisticsHolders",
                newName: "HT_15_Over");

            migrationBuilder.RenameColumn(
                name: "Is_HT_X1",
                table: "AverageStatisticsHolders",
                newName: "HT_05_Over");

            migrationBuilder.RenameColumn(
                name: "Is_FT_X2",
                table: "AverageStatisticsHolders",
                newName: "FT_GG");

            migrationBuilder.RenameColumn(
                name: "Is_FT_X1",
                table: "AverageStatisticsHolders",
                newName: "FT_35_Over");

            migrationBuilder.RenameColumn(
                name: "Is_Corner_FT_X2",
                table: "AverageStatisticsHolders",
                newName: "FT_25_Over");

            migrationBuilder.RenameColumn(
                name: "Is_Corner_FT_X1",
                table: "AverageStatisticsHolders",
                newName: "FT_15_Over");

            migrationBuilder.RenameColumn(
                name: "HT_GG_Home",
                table: "AverageStatisticsHolders",
                newName: "Corner_9_5_Over");

            migrationBuilder.RenameColumn(
                name: "HT_GG_Away",
                table: "AverageStatisticsHolders",
                newName: "Corner_8_5_Over");

            migrationBuilder.RenameColumn(
                name: "HT_15_Over_Home",
                table: "AverageStatisticsHolders",
                newName: "Corner_7_5_Over");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "TeamPerformanceStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StatisticInfoHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PossibleForecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchOddsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ComparisonStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AverageStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AiDataHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
