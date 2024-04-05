using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class matchDateAddedIdentifierDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchIdentifierId",
                table: "PerformanceOverall");

            migrationBuilder.RenameColumn(
                name: "SerialUniqueId",
                table: "PerformanceOverall",
                newName: "SerialUniqueID");

            migrationBuilder.AddColumn<DateTime>(
                name: "MatchDate",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(1921),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 551, DateTimeKind.Local).AddTicks(6019));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(1699),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 551, DateTimeKind.Local).AddTicks(5861));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 904, DateTimeKind.Local).AddTicks(1139),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 553, DateTimeKind.Local).AddTicks(35));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(7349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 552, DateTimeKind.Local).AddTicks(4979));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(7219),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 552, DateTimeKind.Local).AddTicks(4609));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchDate",
                table: "PerformanceOverall");

            migrationBuilder.RenameColumn(
                name: "SerialUniqueID",
                table: "PerformanceOverall",
                newName: "SerialUniqueId");

            migrationBuilder.AddColumn<int>(
                name: "MatchIdentifierId",
                table: "PerformanceOverall",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 551, DateTimeKind.Local).AddTicks(6019),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(1921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 551, DateTimeKind.Local).AddTicks(5861),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(1699));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 553, DateTimeKind.Local).AddTicks(35),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 904, DateTimeKind.Local).AddTicks(1139));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 552, DateTimeKind.Local).AddTicks(4979),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(7349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 3, 12, 13, 58, 552, DateTimeKind.Local).AddTicks(4609),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 3, 15, 11, 17, 903, DateTimeKind.Local).AddTicks(7219));
        }
    }
}
