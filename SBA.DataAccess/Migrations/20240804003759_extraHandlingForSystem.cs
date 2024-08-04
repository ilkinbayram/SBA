using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class extraHandlingForSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bundles_Steps_StepId",
                table: "Bundles");

            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Forecast_ForecastId",
                table: "Predictions");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedSteps_Steps_StartingStepId",
                table: "SavedSteps");

            migrationBuilder.DropTable(
                name: "Forecast");

            migrationBuilder.DropTable(
                name: "MatchIdentifier");

            migrationBuilder.DropIndex(
                name: "IX_SavedSteps_StartingStepId",
                table: "SavedSteps");

            migrationBuilder.DropIndex(
                name: "IX_Predictions_ForecastId",
                table: "Predictions");

            migrationBuilder.DropIndex(
                name: "IX_Country",
                table: "MatchBets");

            migrationBuilder.DropIndex(
                name: "IX_LeagueName",
                table: "MatchBets");

            migrationBuilder.DropIndex(
                name: "IX_SerialUniqueID",
                table: "MatchBets");

            migrationBuilder.DropIndex(
                name: "IX_SerialUniqueID",
                table: "FilterResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerformanceOverall",
                table: "PerformanceOverall");

            migrationBuilder.RenameTable(
                name: "PerformanceOverall",
                newName: "PerformanceOveralls");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Steps",
                newName: "LinkedTo");

            migrationBuilder.RenameColumn(
                name: "ChildId",
                table: "Steps",
                newName: "LinkedFrom");

            migrationBuilder.RenameColumn(
                name: "StepId",
                table: "Bundles",
                newName: "SystemId");

            migrationBuilder.RenameIndex(
                name: "IX_Bundles_StepId",
                table: "Bundles",
                newName: "IX_Bundles_SystemId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBalance",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Steps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(4171),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(752));

            migrationBuilder.AlterColumn<bool>(
                name: "IsSuccess",
                table: "Steps",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "InsuredBetAmount",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Steps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(4171),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(388));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBalance",
                table: "SavedSteps",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "SavedSteps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(7289),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(5721));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "SavedSteps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(7289),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(5307));

            migrationBuilder.AddColumn<int>(
                name: "BetSystemId",
                table: "SavedSteps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Odd",
                table: "Predictions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Predictions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(5734),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(8215));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Predictions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(5734),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(7814));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(903),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 791, DateTimeKind.Local).AddTicks(4087));

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDate",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "LeagueName",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "HomeTeam",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "HT_Under_1_5_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "HT_Over_1_5_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "HT_Match_Result",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "HTWin2_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "HTWin1_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "HTDraw_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Under_3_5_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Under_2_5_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Under_1_5_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Over_3_5_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Over_2_5_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Over_1_5_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_NG_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "FT_Match_Result",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_GG_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_6_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_45_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_23_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_01_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FTWin2_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FTWin1_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "FTDraw_Odd",
                table: "MatchBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(903),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 791, DateTimeKind.Local).AddTicks(3757));

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AwayTeam",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Local).AddTicks(8490),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(2789));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(6401),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 792, DateTimeKind.Local).AddTicks(3071));

            migrationBuilder.AlterColumn<bool>(
                name: "IsShotOnTargetFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsShotFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPossesionFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCornerFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "HomeShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HomeShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HomeShGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HomePossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HomeHtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HomeFtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HomeCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(6401),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 792, DateTimeKind.Local).AddTicks(2674));

            migrationBuilder.AlterColumn<int>(
                name: "AwayShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayShGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayPossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayHtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayFtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOdd",
                table: "ComboBets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ComboBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(3101),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(3249));

            migrationBuilder.AlterColumn<bool>(
                name: "IsInsuredBet",
                table: "ComboBets",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ComboBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(3101),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(2887));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Bundles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(8687));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Bundles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(8275));

            migrationBuilder.AddColumn<int>(
                name: "BundlePriority",
                table: "Bundles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "StartingAmount",
                table: "BetSystems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BetSystems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "BetSystem-B15E");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "BetSystems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(1237),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(6062));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "BetSystems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(1237),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(5659));

            migrationBuilder.AlterColumn<decimal>(
                name: "AcceptedOdd",
                table: "BetSystems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "PerformanceOveralls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(3668),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PerformanceOveralls",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PerformanceOveralls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(3668),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_Home_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_Away_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Conceded_Goals_Home_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Conceded_Goals_Away_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_Home_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_Away_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Conceded_Goals_Home_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Conceded_Goals_Away_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_Home_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_Away_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_GK_Saves_Home_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_GK_Saves_Away_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Conceded_Goals_Home_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Conceded_Goals_Away_Team",
                table: "PerformanceOveralls",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerformanceOveralls",
                table: "PerformanceOveralls",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SavedSteps_BetSystemId",
                table: "SavedSteps",
                column: "BetSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bundles_BetSystems_SystemId",
                table: "Bundles",
                column: "SystemId",
                principalTable: "BetSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedSteps_BetSystems_BetSystemId",
                table: "SavedSteps",
                column: "BetSystemId",
                principalTable: "BetSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bundles_BetSystems_SystemId",
                table: "Bundles");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedSteps_BetSystems_BetSystemId",
                table: "SavedSteps");

            migrationBuilder.DropIndex(
                name: "IX_SavedSteps_BetSystemId",
                table: "SavedSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerformanceOveralls",
                table: "PerformanceOveralls");

            migrationBuilder.DropColumn(
                name: "BetSystemId",
                table: "SavedSteps");

            migrationBuilder.DropColumn(
                name: "BundlePriority",
                table: "Bundles");

            migrationBuilder.RenameTable(
                name: "PerformanceOveralls",
                newName: "PerformanceOverall");

            migrationBuilder.RenameColumn(
                name: "LinkedTo",
                table: "Steps",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "LinkedFrom",
                table: "Steps",
                newName: "ChildId");

            migrationBuilder.RenameColumn(
                name: "SystemId",
                table: "Bundles",
                newName: "StepId");

            migrationBuilder.RenameIndex(
                name: "IX_Bundles_SystemId",
                table: "Bundles",
                newName: "IX_Bundles_StepId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBalance",
                table: "Steps",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Steps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(752),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(4171));

            migrationBuilder.AlterColumn<bool>(
                name: "IsSuccess",
                table: "Steps",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<decimal>(
                name: "InsuredBetAmount",
                table: "Steps",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Steps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(388),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(4171));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBalance",
                table: "SavedSteps",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "SavedSteps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(5721),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(7289));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "SavedSteps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(5307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(7289));

            migrationBuilder.AlterColumn<decimal>(
                name: "Odd",
                table: "Predictions",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Predictions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(8215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(5734));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Predictions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(7814),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(5734));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 791, DateTimeKind.Local).AddTicks(4087),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(903));

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDate",
                table: "MatchBets",
                type: "datetime2(7)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "LeagueName",
                table: "MatchBets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HomeTeam",
                table: "MatchBets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HT_Under_1_5_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HT_Over_1_5_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "HT_Match_Result",
                table: "MatchBets",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HTWin2_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HTWin1_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HTDraw_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Under_3_5_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Under_2_5_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Under_1_5_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Over_3_5_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Over_2_5_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_Over_1_5_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_NG_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "FT_Match_Result",
                table: "MatchBets",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_GG_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_6_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_45_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_23_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_01_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FTWin2_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FTWin1_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FTDraw_Odd",
                table: "MatchBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 791, DateTimeKind.Local).AddTicks(3757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(903));

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "MatchBets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AwayTeam",
                table: "MatchBets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(2789),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Local).AddTicks(8490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 792, DateTimeKind.Local).AddTicks(3071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(6401));

            migrationBuilder.AlterColumn<bool>(
                name: "IsShotOnTargetFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsShotFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPossesionFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCornerFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "HomeShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HomeShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HomeShGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HomePossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HomeHtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HomeFtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HomeCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 792, DateTimeKind.Local).AddTicks(2674),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(6401));

            migrationBuilder.AlterColumn<int>(
                name: "AwayShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayShGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayPossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayHtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayFtGoalCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOdd",
                table: "ComboBets",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ComboBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(3249),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(3101));

            migrationBuilder.AlterColumn<bool>(
                name: "IsInsuredBet",
                table: "ComboBets",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ComboBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(2887),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(3101));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Bundles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(8687),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Bundles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(8275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 265, DateTimeKind.Unspecified).AddTicks(180));

            migrationBuilder.AlterColumn<decimal>(
                name: "StartingAmount",
                table: "BetSystems",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BetSystems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "BetSystem-B15E",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "BetSystems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(6062),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(1237));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "BetSystems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(5659),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 264, DateTimeKind.Unspecified).AddTicks(1237));

            migrationBuilder.AlterColumn<decimal>(
                name: "AcceptedOdd",
                table: "BetSystems",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(3668));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PerformanceOverall",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 4, 37, 58, 263, DateTimeKind.Unspecified).AddTicks(3668));

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_Home_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_Away_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Conceded_Goals_Home_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Conceded_Goals_Away_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_Home_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_Away_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Conceded_Goals_Home_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Conceded_Goals_Away_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_Home_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_Away_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_GK_Saves_Home_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_GK_Saves_Away_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Conceded_Goals_Home_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Conceded_Goals_Away_Team",
                table: "PerformanceOverall",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerformanceOverall",
                table: "PerformanceOverall",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MatchIdentifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AwayTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FT_Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HT_Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Serial = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchIdentifier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forecast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Is99Percent = table.Column<bool>(type: "bit", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Serial = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forecast_MatchIdentifier_MatchIdentifierId",
                        column: x => x.MatchIdentifierId,
                        principalTable: "MatchIdentifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedSteps_StartingStepId",
                table: "SavedSteps",
                column: "StartingStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_ForecastId",
                table: "Predictions",
                column: "ForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_Country",
                table: "MatchBets",
                column: "Country")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_LeagueName",
                table: "MatchBets",
                column: "LeagueName")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_SerialUniqueID",
                table: "MatchBets",
                column: "SerialUniqueID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_SerialUniqueID",
                table: "FilterResults",
                column: "SerialUniqueID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Forecast_MatchIdentifierId",
                table: "Forecast",
                column: "MatchIdentifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bundles_Steps_StepId",
                table: "Bundles",
                column: "StepId",
                principalTable: "Steps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Forecast_ForecastId",
                table: "Predictions",
                column: "ForecastId",
                principalTable: "Forecast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedSteps_Steps_StartingStepId",
                table: "SavedSteps",
                column: "StartingStepId",
                principalTable: "Steps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
