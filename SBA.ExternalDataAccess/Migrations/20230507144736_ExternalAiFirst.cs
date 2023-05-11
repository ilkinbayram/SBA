using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class ExternalAiFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AiDataHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serial = table.Column<int>(type: "int", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    JsonTextContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 59, DateTimeKind.Local).AddTicks(7924)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 59, DateTimeKind.Local).AddTicks(8088)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiDataHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeagueStatisticsHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountFound = table.Column<int>(type: "int", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LeagueName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateOfAnalyse = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local)),
                    FT_GoalsAverage = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    HT_GoalsAverage = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    SH_GoalsAverage = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    GG_Percentage = table.Column<int>(type: "int", nullable: false),
                    FT_Over15_Percentage = table.Column<int>(type: "int", nullable: false),
                    FT_Over25_Percentage = table.Column<int>(type: "int", nullable: false),
                    FT_Over35_Percentage = table.Column<int>(type: "int", nullable: false),
                    HT_Over05_Percentage = table.Column<int>(type: "int", nullable: false),
                    HT_Over15_Percentage = table.Column<int>(type: "int", nullable: false),
                    SH_Over05_Percentage = table.Column<int>(type: "int", nullable: false),
                    SH_Over15_Percentage = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 55, DateTimeKind.Local).AddTicks(2382)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 55, DateTimeKind.Local).AddTicks(2551)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueStatisticsHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchIdentifiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serial = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    HomeTeam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AwayTeam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatchDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Local)),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 55, DateTimeKind.Local).AddTicks(4907)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 55, DateTimeKind.Local).AddTicks(5052)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchIdentifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchOddsHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serial = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FT_Win1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_FT_Home_Home = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_FT_Home_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_FT_Home_Away = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_FT_Draw_Home = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_FT_Draw_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_FT_Draw_Away = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_FT_Away_Home = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_FT_Away_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_FT_Away_Away = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win1_Under_15 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Draw_Under_15 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win2_Under_15 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win1_Over_15 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Draw_Over_15 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win2_Over_15 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win1_Under_25 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Draw_Under_25 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win2_Under_25 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win1_Over_25 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Draw_Over_25 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win2_Over_25 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win1_Under_35 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Draw_Under_35 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win2_Under_35 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win1_Over_35 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Draw_Over_35 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win2_Over_35 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win1_Under_45 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Draw_Under_45 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win2_Under_45 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win1_Over_45 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Draw_Over_45 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Win2_Over_45 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_04_Win1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_04_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_04_Win2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_03_Win1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_03_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_03_Win2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_02_Win1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_02_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_02_Win2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_01_Win1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_01_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_01_Win2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_40_Win1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_40_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_40_Win2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_30_Win1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_30_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_30_Win2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_20_Win1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_20_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_20_Win2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_10_Win1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_10_Draw = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Handicap_10_Win2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Double_1_X = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Double_1_2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_Double_X_2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FirstGoal_Home = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FirstGoal_None = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FirstGoal_Away = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_4_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_4_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_5_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    FT_5_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_0_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_0_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_2_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    HT_2_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Home_2_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Home_2_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Home_3_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Home_3_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Home_4_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Home_4_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Away_2_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Away_2_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Away_3_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Away_3_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Away_4_5_Under = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Away_4_5_Over = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Even_Tek = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Odd_Cut = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_0_0 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_1_0 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_2_0 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_3_0 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_4_0 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_5_0 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_6_0 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_0_1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_0_2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_0_3 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_0_4 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_0_5 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_0_6 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_1_1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_2_1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_3_1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_4_1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_5_1 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_1_2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_1_3 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_1_4 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_1_5 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_2_2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_3_2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_4_2 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_2_3 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_2_4 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_3_3 = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Score_Other = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    MoreGoal_1st = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    MoreGoal_Equal = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    MoreGoal_2nd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
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
                    Goals6 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 55, DateTimeKind.Local).AddTicks(7620)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 55, DateTimeKind.Local).AddTicks(7864)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOddsHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticInfoHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Serial = table.Column<int>(type: "int", nullable: false),
                    StatisticType = table.Column<int>(type: "int", nullable: false),
                    BySideType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomePercent = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    AwayPercent = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticInfoHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AverageStatisticsHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BySideType = table.Column<int>(type: "int", nullable: false),
                    LeagueStaisticsHolderId = table.Column<int>(type: "int", nullable: false),
                    Average_FT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_HT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_HT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_SH_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_SH_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Corners_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Corners_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Shut_HomeTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_Shut_AwayTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_ShutOnTarget_HomeTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_ShutOnTarget_AwayTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_Possesion = table.Column<int>(type: "int", nullable: false),
                    Away_Possesion = table.Column<int>(type: "int", nullable: false),
                    Is_FT_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_FT_X = table.Column<int>(type: "int", nullable: false),
                    Is_FT_Win2 = table.Column<int>(type: "int", nullable: false),
                    Is_HT_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_HT_X = table.Column<int>(type: "int", nullable: false),
                    Is_HT_Win2 = table.Column<int>(type: "int", nullable: false),
                    Is_SH_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_SH_X = table.Column<int>(type: "int", nullable: false),
                    Is_SH_Win2 = table.Column<int>(type: "int", nullable: false),
                    Corner_Home_3_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Home_4_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Home_5_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Away_3_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Away_4_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Away_5_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_7_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_8_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_9_5_Over = table.Column<int>(type: "int", nullable: false),
                    Is_Corner_FT_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_Corner_FT_X = table.Column<int>(type: "int", nullable: false),
                    Is_Corner_FT_Win2 = table.Column<int>(type: "int", nullable: false),
                    FT_GG = table.Column<int>(type: "int", nullable: false),
                    SH_GG = table.Column<int>(type: "int", nullable: false),
                    HT_GG = table.Column<int>(type: "int", nullable: false),
                    FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    FT_25_Over = table.Column<int>(type: "int", nullable: false),
                    FT_35_Over = table.Column<int>(type: "int", nullable: false),
                    HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    HT_15_Over = table.Column<int>(type: "int", nullable: false),
                    SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    SH_15_Over = table.Column<int>(type: "int", nullable: false),
                    Home_HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Home_HT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Home_SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    Home_SH_15_Over = table.Column<int>(type: "int", nullable: false),
                    Home_FT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Home_FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Home_Win_Any_Half = table.Column<int>(type: "int", nullable: false),
                    Away_HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Away_HT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Away_SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    Away_SH_15_Over = table.Column<int>(type: "int", nullable: false),
                    Away_FT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Away_FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Away_Win_Any_Half = table.Column<int>(type: "int", nullable: false),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    UniqueIdentity = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 54, DateTimeKind.Local).AddTicks(7797)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 54, DateTimeKind.Local).AddTicks(8024)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AverageStatisticsHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AverageStatisticsHolders_LeagueStatisticsHolders_LeagueStaisticsHolderId",
                        column: x => x.LeagueStaisticsHolderId,
                        principalTable: "LeagueStatisticsHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonStatisticsHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BySideType = table.Column<int>(type: "int", nullable: false),
                    LeagueStaisticsHolderId = table.Column<int>(type: "int", nullable: false),
                    Average_FT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_HT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_HT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_SH_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_SH_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Corners_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Corners_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Shut_HomeTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_Shut_AwayTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_ShutOnTarget_HomeTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_ShutOnTarget_AwayTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_Possesion = table.Column<int>(type: "int", nullable: false),
                    Away_Possesion = table.Column<int>(type: "int", nullable: false),
                    Is_FT_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_FT_X = table.Column<int>(type: "int", nullable: false),
                    Is_FT_Win2 = table.Column<int>(type: "int", nullable: false),
                    Is_HT_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_HT_X = table.Column<int>(type: "int", nullable: false),
                    Is_HT_Win2 = table.Column<int>(type: "int", nullable: false),
                    Is_SH_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_SH_X = table.Column<int>(type: "int", nullable: false),
                    Is_SH_Win2 = table.Column<int>(type: "int", nullable: false),
                    Corner_Home_3_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Home_4_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Home_5_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Away_3_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Away_4_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Away_5_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_7_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_8_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_9_5_Over = table.Column<int>(type: "int", nullable: false),
                    Is_Corner_FT_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_Corner_FT_X = table.Column<int>(type: "int", nullable: false),
                    Is_Corner_FT_Win2 = table.Column<int>(type: "int", nullable: false),
                    FT_GG = table.Column<int>(type: "int", nullable: false),
                    SH_GG = table.Column<int>(type: "int", nullable: false),
                    HT_GG = table.Column<int>(type: "int", nullable: false),
                    FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    FT_25_Over = table.Column<int>(type: "int", nullable: false),
                    FT_35_Over = table.Column<int>(type: "int", nullable: false),
                    HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    HT_15_Over = table.Column<int>(type: "int", nullable: false),
                    SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    SH_15_Over = table.Column<int>(type: "int", nullable: false),
                    Home_HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Home_HT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Home_SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    Home_SH_15_Over = table.Column<int>(type: "int", nullable: false),
                    Home_FT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Home_FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Home_Win_Any_Half = table.Column<int>(type: "int", nullable: false),
                    Away_HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Away_HT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Away_SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    Away_SH_15_Over = table.Column<int>(type: "int", nullable: false),
                    Away_FT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Away_FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Away_Win_Any_Half = table.Column<int>(type: "int", nullable: false),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    UniqueIdentity = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 53, DateTimeKind.Local).AddTicks(5212)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 53, DateTimeKind.Local).AddTicks(5485)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonStatisticsHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComparisonStatisticsHolders_LeagueStatisticsHolders_LeagueStaisticsHolderId",
                        column: x => x.LeagueStaisticsHolderId,
                        principalTable: "LeagueStatisticsHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPerformanceStatisticsHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BySideType = table.Column<int>(type: "int", nullable: false),
                    HomeOrAway = table.Column<int>(type: "int", nullable: false),
                    LeagueStaisticsHolderId = table.Column<int>(type: "int", nullable: false),
                    Average_FT_Goals_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_HT_Goals_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_SH_Goals_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Corners_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Shut_Team = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_ShutOnTarget_Team = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Team_Possesion = table.Column<int>(type: "int", nullable: false),
                    Is_FT_Win = table.Column<int>(type: "int", nullable: false),
                    Is_FT_X = table.Column<int>(type: "int", nullable: false),
                    Is_HT_Win = table.Column<int>(type: "int", nullable: false),
                    Is_HT_X = table.Column<int>(type: "int", nullable: false),
                    Is_SH_Win = table.Column<int>(type: "int", nullable: false),
                    Is_SH_X = table.Column<int>(type: "int", nullable: false),
                    Corner_Team_3_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Team_4_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_Team_5_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_7_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_8_5_Over = table.Column<int>(type: "int", nullable: false),
                    Corner_9_5_Over = table.Column<int>(type: "int", nullable: false),
                    Is_Corner_FT_Win = table.Column<int>(type: "int", nullable: false),
                    Is_Corner_FT_X = table.Column<int>(type: "int", nullable: false),
                    FT_GG = table.Column<int>(type: "int", nullable: false),
                    SH_GG = table.Column<int>(type: "int", nullable: false),
                    HT_GG = table.Column<int>(type: "int", nullable: false),
                    FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    FT_25_Over = table.Column<int>(type: "int", nullable: false),
                    FT_35_Over = table.Column<int>(type: "int", nullable: false),
                    HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    HT_15_Over = table.Column<int>(type: "int", nullable: false),
                    SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    SH_15_Over = table.Column<int>(type: "int", nullable: false),
                    Team_HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Team_HT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Team_SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    Team_SH_15_Over = table.Column<int>(type: "int", nullable: false),
                    Team_FT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Team_FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Team_Win_Any_Half = table.Column<int>(type: "int", nullable: false),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    UniqueIdentity = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 54, DateTimeKind.Local).AddTicks(2783)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 7, 18, 47, 30, 54, DateTimeKind.Local).AddTicks(2974)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPerformanceStatisticsHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPerformanceStatisticsHolders_LeagueStatisticsHolders_LeagueStaisticsHolderId",
                        column: x => x.LeagueStaisticsHolderId,
                        principalTable: "LeagueStatisticsHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AverageStatisticsHolders_LeagueStaisticsHolderId",
                table: "AverageStatisticsHolders",
                column: "LeagueStaisticsHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonStatisticsHolders_LeagueStaisticsHolderId",
                table: "ComparisonStatisticsHolders",
                column: "LeagueStaisticsHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchIdentifiers_Serial",
                table: "MatchIdentifiers",
                column: "Serial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamPerformanceStatisticsHolders_LeagueStaisticsHolderId",
                table: "TeamPerformanceStatisticsHolders",
                column: "LeagueStaisticsHolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiDataHolders");

            migrationBuilder.DropTable(
                name: "AverageStatisticsHolders");

            migrationBuilder.DropTable(
                name: "ComparisonStatisticsHolders");

            migrationBuilder.DropTable(
                name: "MatchIdentifiers");

            migrationBuilder.DropTable(
                name: "MatchOddsHolders");

            migrationBuilder.DropTable(
                name: "StatisticInfoHolders");

            migrationBuilder.DropTable(
                name: "TeamPerformanceStatisticsHolders");

            migrationBuilder.DropTable(
                name: "LeagueStatisticsHolders");
        }
    }
}
