using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class perfNewDbResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 702, DateTimeKind.Local).AddTicks(8677),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 3, DateTimeKind.Local).AddTicks(9051));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 702, DateTimeKind.Local).AddTicks(8541),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 3, DateTimeKind.Local).AddTicks(8990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(6655),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 4, DateTimeKind.Local).AddTicks(2022));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(3933),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 4, DateTimeKind.Local).AddTicks(643));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(3797),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 4, DateTimeKind.Local).AddTicks(589));

            migrationBuilder.CreateTable(
                name: "PerformanceOverall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Average_FT_Goals_Home_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Goals_Away_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_HT_Goals_Home_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_HT_Goals_Away_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_SH_Goals_Home_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_SH_Goals_Away_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Conceded_Goals_Home_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Conceded_Goals_Away_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_HT_Conceded_Goals_Home_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_HT_Conceded_Goals_Away_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_SH_Conceded_Goals_Home_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_SH_Conceded_Goals_Away_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_GK_Saves_Home_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_GK_Saves_Away_Team = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Average_FT_Shut_Home_Team = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_Shut_Away_Team = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_ShutOnTarget_Home_Team = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Average_FT_ShutOnTarget_Away_Team = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Home_Team_Possesion = table.Column<int>(type: "int", nullable: false),
                    Away_Team_Possesion = table.Column<int>(type: "int", nullable: false),
                    Is_FT_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_FT_Win2 = table.Column<int>(type: "int", nullable: false),
                    Is_FT_X = table.Column<int>(type: "int", nullable: false),
                    Is_HT_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_HT_Win2 = table.Column<int>(type: "int", nullable: false),
                    Is_HT_X = table.Column<int>(type: "int", nullable: false),
                    Is_SH_Win1 = table.Column<int>(type: "int", nullable: false),
                    Is_SH_Win2 = table.Column<int>(type: "int", nullable: false),
                    Is_SH_X = table.Column<int>(type: "int", nullable: false),
                    FT_GG_Home = table.Column<int>(type: "int", nullable: false),
                    FT_GG_Away = table.Column<int>(type: "int", nullable: false),
                    FT_15_Over_Home = table.Column<int>(type: "int", nullable: false),
                    FT_25_Over_Home = table.Column<int>(type: "int", nullable: false),
                    FT_35_Over_Home = table.Column<int>(type: "int", nullable: false),
                    HT_05_Over_Home = table.Column<int>(type: "int", nullable: false),
                    SH_05_Over_Home = table.Column<int>(type: "int", nullable: false),
                    FT_15_Over_Away = table.Column<int>(type: "int", nullable: false),
                    FT_25_Over_Away = table.Column<int>(type: "int", nullable: false),
                    FT_35_Over_Away = table.Column<int>(type: "int", nullable: false),
                    HT_05_Over_Away = table.Column<int>(type: "int", nullable: false),
                    SH_05_Over_Away = table.Column<int>(type: "int", nullable: false),
                    Home_Team_HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Home_Team_SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    Home_Team_FT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Home_Team_FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Away_Team_HT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Away_Team_SH_05_Over = table.Column<int>(type: "int", nullable: false),
                    Away_Team_FT_05_Over = table.Column<int>(type: "int", nullable: false),
                    Away_Team_FT_15_Over = table.Column<int>(type: "int", nullable: false),
                    Home_Team_Win_Any_Half = table.Column<int>(type: "int", nullable: false),
                    Away_Team_Win_Any_Half = table.Column<int>(type: "int", nullable: false),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false),
                    SerialUniqueId = table.Column<int>(type: "int", nullable: false),
                    Hashed_Full_Detailed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hashed_Detailed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hashed_Compact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hashed_Simple = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hashed_Less_Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceOverall", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceOverall");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 3, DateTimeKind.Local).AddTicks(9051),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 702, DateTimeKind.Local).AddTicks(8677));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 3, DateTimeKind.Local).AddTicks(8990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 702, DateTimeKind.Local).AddTicks(8541));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 4, DateTimeKind.Local).AddTicks(2022),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(6655));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 4, DateTimeKind.Local).AddTicks(643),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(3933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 18, 44, 49, 4, DateTimeKind.Local).AddTicks(589),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 2, 23, 35, 35, 703, DateTimeKind.Local).AddTicks(3797));
        }
    }
}
