using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class defaultsAreRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Team_Win_Any_Half",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Team_SH_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Team_SH_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Team_HT_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Team_HT_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Team_FT_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Team_FT_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "SH_GG",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "SH_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "SH_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_X",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_X",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_X",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_X",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_GG",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_GG",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_35_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_25_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Team_5_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Team_4_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Team_3_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_9_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_8_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_7_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "BySideType",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<int>(
                name: "SH_Over15_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "SH_Over05_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<decimal>(
                name: "SH_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<int>(
                name: "HT_Over15_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_Over05_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<decimal>(
                name: "HT_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<int>(
                name: "GG_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_Over35_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_Over25_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_Over15_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<int>(
                name: "CountFound",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "SH_GG",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "SH_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "SH_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_X",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win2",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win1",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_X",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win2",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win1",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_X",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win2",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win1",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_X",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win2",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win1",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_Win_Any_Half",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_SH_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_SH_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_HT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_HT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_FT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_FT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_GG",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_GG",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_35_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_25_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_5_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_4_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_3_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_5_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_4_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_3_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_9_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_8_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_7_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "BySideType",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_Win_Any_Half",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_SH_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_SH_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_HT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_HT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_FT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_FT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<int>(
                name: "SH_GG",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "SH_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "SH_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_X",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win2",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win1",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_X",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win2",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win1",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_X",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win2",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win1",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_X",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win2",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win1",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_Win_Any_Half",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_SH_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_SH_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_HT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_HT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_FT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Home_FT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_GG",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_GG",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_35_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_25_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "FT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_5_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_4_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_3_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_5_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_4_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_3_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_9_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_8_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Corner_7_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "BySideType",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_Win_Any_Half",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_SH_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_SH_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_HT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_HT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_FT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Away_FT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Team_Win_Any_Half",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Team_SH_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Team_SH_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Team_HT_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Team_HT_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Team_FT_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Team_FT_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SH_GG",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SH_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SH_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_X",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_X",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_X",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_X",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_GG",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_05_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_GG",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_35_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_25_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_15_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Team_5_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Team_4_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Team_3_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_9_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_8_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_7_5_Over",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BySideType",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "SH_Over15_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SH_Over05_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "SH_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "HT_Over15_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_Over05_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "HT_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "GG_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_Over35_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_Over25_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_Over15_Percentage",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "CountFound",
                table: "LeagueStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SH_GG",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SH_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SH_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_X",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win2",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win1",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_X",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win2",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win1",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_X",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win2",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win1",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_X",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win2",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win1",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_Win_Any_Half",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_SH_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_SH_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_HT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_HT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_FT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_FT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_GG",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_GG",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_35_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_25_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_5_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_4_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_3_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_5_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_4_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_3_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_9_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_8_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_7_5_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BySideType",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_Win_Any_Half",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_SH_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_SH_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_HT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_HT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_FT_15_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_FT_05_Over",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "SH_GG",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SH_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SH_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_X",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win2",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_SH_Win1",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_X",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win2",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_HT_Win1",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_X",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win2",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_FT_Win1",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_X",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win2",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Is_Corner_FT_Win1",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_Win_Any_Half",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_SH_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_SH_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_HT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_HT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_FT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Home_FT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_GG",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_GG",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_35_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_25_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_5_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_4_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Home_3_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_5_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_4_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_Away_3_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_9_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_8_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Corner_7_5_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BySideType",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_Win_Any_Half",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_SH_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_SH_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_HT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_HT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_FT_15_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Away_FT_05_Over",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);
        }
    }
}
