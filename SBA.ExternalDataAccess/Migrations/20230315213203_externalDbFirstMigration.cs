using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class externalDbFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeagueStatisticsHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountFound = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LeagueName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateOfAnalyse = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Local)),
                    FT_GoalsAverage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HT_GoalsAverage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SH_GoalsAverage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GG_Percentage = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_Over15_Percentage = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_Over25_Percentage = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_Over35_Percentage = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    HT_Over05_Percentage = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    HT_Over15_Percentage = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    SH_Over05_Percentage = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    SH_Over15_Percentage = table.Column<int>(type: "int", nullable: false, defaultValue: -1)
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
                    MatchDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Local))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchIdentifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticsHolder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BySideType = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    ComparisonType = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    StatisticType = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    Average_FT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: -1m),
                    Average_FT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: -1m),
                    Average_HT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: -1m),
                    Average_HT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: -1m),
                    Average_SH_Goals_HomeTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: -1m),
                    Average_SH_Goals_AwayTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: -1m),
                    Average_FT_Corners_HomeTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: -1m),
                    Average_FT_Corners_AwayTeam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: -1m),
                    Is_FT_Win1 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_FT_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_FT_Win2 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_HT_Win1 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_HT_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_HT_Win2 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_SH_Win1 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_SH_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_SH_Win2 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Home_3_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Home_4_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Home_5_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Away_3_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Away_4_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Away_5_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_7_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_8_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_9_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_Corner_FT_Win1 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_Corner_FT_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_Corner_FT_Win2 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_GG = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    SH_GG = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    HT_GG = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_25_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_35_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    HT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    HT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    SH_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    SH_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_HT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_HT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_SH_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_SH_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_FT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_FT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_Win_Any_Half = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_HT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_HT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_SH_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_SH_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_FT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_FT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_Win_Any_Half = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    LeagueStatisticsHolderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsHolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticsHolder_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                        column: x => x.LeagueStatisticsHolderId,
                        principalTable: "LeagueStatisticsHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsHolder_LeagueStatisticsHolderId",
                table: "StatisticsHolder",
                column: "LeagueStatisticsHolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchIdentifiers");

            migrationBuilder.DropTable(
                name: "StatisticsHolder");

            migrationBuilder.DropTable(
                name: "LeagueStatisticsHolders");
        }
    }
}
