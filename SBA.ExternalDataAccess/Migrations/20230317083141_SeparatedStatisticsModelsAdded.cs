using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class SeparatedStatisticsModelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticsHolder");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "AverageStatisticsHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BySideType = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    LeagueStaisticsHolderId = table.Column<int>(type: "int", nullable: false),
                    Average_FT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_HT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_HT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_SH_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_SH_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Corners_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Corners_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
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
                    table.PrimaryKey("PK_AverageStatisticsHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AverageStatisticsHolders_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                        column: x => x.LeagueStatisticsHolderId,
                        principalTable: "LeagueStatisticsHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonStatisticsHolder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BySideType = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    LeagueStaisticsHolderId = table.Column<int>(type: "int", nullable: false),
                    Average_FT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_HT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_HT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_SH_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_SH_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Corners_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Corners_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
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
                    table.PrimaryKey("PK_ComparisonStatisticsHolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComparisonStatisticsHolder_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                        column: x => x.LeagueStatisticsHolderId,
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
                    BySideType = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    HomeOrAway = table.Column<int>(type: "int", nullable: false),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    LeagueStaisticsHolderId = table.Column<int>(type: "int", nullable: false),
                    Average_FT_Goals_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_HT_Goals_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_SH_Goals_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Corners_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Is_FT_Win = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_FT_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_HT_Win = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_HT_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_SH_Win = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_SH_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Team_3_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Team_4_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Team_5_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_7_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_8_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_9_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_Corner_FT_Win = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_Corner_FT_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
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
                    Team_HT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Team_HT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Team_SH_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Team_SH_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Team_FT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Team_FT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Team_Win_Any_Half = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    LeagueStatisticsHolderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPerformanceStatisticsHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPerformanceStatisticsHolders_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                        column: x => x.LeagueStatisticsHolderId,
                        principalTable: "LeagueStatisticsHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AverageStatisticsHolders_LeagueStatisticsHolderId",
                table: "AverageStatisticsHolders",
                column: "LeagueStatisticsHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonStatisticsHolder_LeagueStatisticsHolderId",
                table: "ComparisonStatisticsHolder",
                column: "LeagueStatisticsHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPerformanceStatisticsHolders_LeagueStatisticsHolderId",
                table: "TeamPerformanceStatisticsHolders",
                column: "LeagueStatisticsHolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AverageStatisticsHolders");

            migrationBuilder.DropTable(
                name: "ComparisonStatisticsHolder");

            migrationBuilder.DropTable(
                name: "TeamPerformanceStatisticsHolders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "StatisticsHolder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueStatisticsHolderId = table.Column<int>(type: "int", nullable: false),
                    Average_FT_Corners_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Corners_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_FT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_HT_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_HT_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_SH_Goals_AwayTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Average_SH_Goals_HomeTeam = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: -1m),
                    Away_FT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_FT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_HT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_HT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_SH_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_SH_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Away_Win_Any_Half = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    BySideType = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    ComparisonType = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_7_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_8_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_9_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Away_3_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Away_4_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Away_5_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Home_3_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Home_4_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Corner_Home_5_5_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_25_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_35_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    FT_GG = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    HT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    HT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    HT_GG = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_FT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_FT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_HT_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_HT_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_SH_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_SH_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Home_Win_Any_Half = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_Corner_FT_Win1 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_Corner_FT_Win2 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_Corner_FT_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_FT_Win1 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_FT_Win2 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_FT_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_HT_Win1 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_HT_Win2 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_HT_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_SH_Win1 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_SH_Win2 = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    Is_SH_X = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    SH_05_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    SH_15_Over = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    SH_GG = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    StatisticType = table.Column<int>(type: "int", nullable: false, defaultValue: -1)
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
    }
}
