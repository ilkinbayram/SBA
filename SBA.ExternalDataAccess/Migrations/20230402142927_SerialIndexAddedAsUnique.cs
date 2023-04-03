using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class SerialIndexAddedAsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_MatchIdentifiers_Serial",
                table: "MatchIdentifiers",
                column: "Serial",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MatchIdentifiers_Serial",
                table: "MatchIdentifiers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MatchDateTime",
                table: "MatchIdentifiers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAnalyse",
                table: "LeagueStatisticsHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
