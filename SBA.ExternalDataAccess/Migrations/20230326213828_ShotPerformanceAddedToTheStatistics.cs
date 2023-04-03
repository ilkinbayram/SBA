using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class ShotPerformanceAddedToTheStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_ShutOnTarget_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_Shut_Team",
                table: "TeamPerformanceStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Team_Possesion",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_ShutOnTarget_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_ShutOnTarget_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_Shut_AwayTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_Shut_HomeTeam",
                table: "ComparisonStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Away_Possesion",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Home_Possesion",
                table: "ComparisonStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_ShutOnTarget_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_ShutOnTarget_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_Shut_AwayTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Average_FT_Shut_HomeTeam",
                table: "AverageStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Away_Possesion",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Home_Possesion",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MatchOddsHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serial = table.Column<int>(type: "int", nullable: false),
                    FT_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_FT_Home_Home = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_FT_Home_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_FT_Home_Away = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_FT_Draw_Home = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_FT_Draw_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_FT_Draw_Away = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_FT_Away_Home = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_FT_Away_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_FT_Away_Away = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win1_Under_15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Draw_Under_15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win2_Under_15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win1_Over_15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Draw_Over_15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win2_Over_15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win1_Under_25 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Draw_Under_25 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win2_Under_25 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win1_Over_25 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Draw_Over_25 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win2_Over_25 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win1_Under_35 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Draw_Under_35 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win2_Under_35 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win1_Over_35 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Draw_Over_35 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win2_Over_35 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win1_Under_45 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Draw_Under_45 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win2_Under_45 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win1_Over_45 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Draw_Over_45 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Win2_Over_45 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_04_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_04_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_04_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_03_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_03_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_03_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_02_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_02_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_02_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_01_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_01_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_01_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_40_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_40_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_40_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_30_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_30_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_30_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_20_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_20_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_20_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_10_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_10_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Handicap_10_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Double_1_X = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Double_1_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Double_X_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FirstGoal_Home = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FirstGoal_None = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FirstGoal_Away = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_4_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_4_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_5_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_5_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_0_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_0_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_2_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_2_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_2_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_2_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_3_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_3_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_4_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_4_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Away_2_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Away_2_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Away_3_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Away_3_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Away_4_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Away_4_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Even_Tek = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Odd_Cut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_0_0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_1_0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_2_0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_3_0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_4_0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_5_0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_6_0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_0_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_0_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_0_3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_0_4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_0_5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_0_6 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_1_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_2_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_3_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_4_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_5_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_1_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_1_3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_1_4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_1_5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_2_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_3_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_4_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_2_3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_2_4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_3_3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score_Other = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoreGoal_1st = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoreGoal_Equal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoreGoal_2nd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_4_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_4_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_8_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_8_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_9_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_9_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_10_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_10_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_MoreCorner_Home = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_MoreCorner_Equal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_MoreCorner_Away = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_MoreCorner_Home = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_MoreCorner_Equal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_MoreCorner_Away = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FirstCorner_Home = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FirstCorner_Never = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FirstCorner_Away = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Corners_Range_0_8 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Corners_Range_9_11 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_Corners_Range_12 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_Range_0_4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_Range_5_6 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Corners_Range_7 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cards_3_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cards_3_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cards_4_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cards_4_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cards_5_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cards_5_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SH_Win1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SH_Draw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SH_Win2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Double_1_X = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Double_1_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_Double_X_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_1_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_1_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Away_1_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Away_1_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_1_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_1_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_1_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_1_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_2_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_2_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_3_5_Under = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_3_5_Over = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_GG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FT_NG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Goals01 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Goals23 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Goals45 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Goals6 = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOddsHolders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchOddsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_ShutOnTarget_Team",
                table: "TeamPerformanceStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_Shut_Team",
                table: "TeamPerformanceStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Team_Possesion",
                table: "TeamPerformanceStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_ShutOnTarget_AwayTeam",
                table: "ComparisonStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_ShutOnTarget_HomeTeam",
                table: "ComparisonStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_Shut_AwayTeam",
                table: "ComparisonStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_Shut_HomeTeam",
                table: "ComparisonStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Away_Possesion",
                table: "ComparisonStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Home_Possesion",
                table: "ComparisonStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_ShutOnTarget_AwayTeam",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_ShutOnTarget_HomeTeam",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_Shut_AwayTeam",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Average_FT_Shut_HomeTeam",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Away_Possesion",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "Home_Possesion",
                table: "AverageStatisticsHolders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
