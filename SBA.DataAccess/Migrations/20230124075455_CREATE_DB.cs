using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class CREATE_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilterResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialUniqueID = table.Column<int>(type: "int", nullable: false),
                    HT_Result = table.Column<int>(type: "int", nullable: false),
                    SH_Result = table.Column<int>(type: "int", nullable: false),
                    FT_Result = table.Column<int>(type: "int", nullable: false),
                    MoreGoalsBetweenTimes = table.Column<int>(type: "int", nullable: false),
                    Home_HT_0_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Home_HT_1_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Home_SH_0_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Home_SH_1_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Away_HT_0_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Away_HT_1_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Away_SH_0_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Away_SH_1_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Away_Win_Any_Half = table.Column<bool>(type: "bit", nullable: false),
                    Home_Win_Any_Half = table.Column<bool>(type: "bit", nullable: false),
                    Away_FT_0_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Away_FT_1_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Home_FT_0_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    Home_FT_1_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    HT_FT_Result = table.Column<int>(type: "int", nullable: false),
                    HT_0_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    HT_1_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    SH_0_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    SH_1_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    FT_1_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    FT_2_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    FT_3_5_Over = table.Column<bool>(type: "bit", nullable: false),
                    FT_GG = table.Column<bool>(type: "bit", nullable: false),
                    HT_GG = table.Column<bool>(type: "bit", nullable: false),
                    SH_GG = table.Column<bool>(type: "bit", nullable: false),
                    FT_TotalBetween = table.Column<int>(type: "int", nullable: false),
                    ModelType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchBets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SerialUniqueID = table.Column<int>(type: "int", nullable: false),
                    LeagueName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HomeTeam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AwayTeam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HT_Match_Result = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FT_Match_Result = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FTWin1_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FTDraw_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FTWin2_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    HTWin1_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    HTDraw_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    HTWin2_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    HT_Under_1_5_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    HT_Over_1_5_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_Under_1_5_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_Over_1_5_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_Under_2_5_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_Over_2_5_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_Under_3_5_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_Over_3_5_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_GG_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_NG_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_01_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_23_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_45_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    FT_6_Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2(7)", precision: 7, scale: 2, nullable: false),
                    ModelType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchBets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterResults");

            migrationBuilder.DropTable(
                name: "MatchBets");
        }
    }
}
