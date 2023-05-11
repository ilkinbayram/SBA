using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class UserIdentityDateTimeLoggerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "System.Admin");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 845, DateTimeKind.Local).AddTicks(8735));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "System.Admin");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 845, DateTimeKind.Local).AddTicks(8920));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FilterResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "System.Admin");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 846, DateTimeKind.Local).AddTicks(3156));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "FilterResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "System.Admin");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 846, DateTimeKind.Local).AddTicks(3418));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MatchBets");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "MatchBets");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "MatchBets");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "MatchBets");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "FilterResults");
        }
    }
}
