using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class StringLengthUpdatedForLogSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 18, 0, 22, 638, DateTimeKind.Local).AddTicks(8805),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 845, DateTimeKind.Local).AddTicks(8920));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "MatchBets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System.Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "System.Admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 18, 0, 22, 638, DateTimeKind.Local).AddTicks(8659),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 845, DateTimeKind.Local).AddTicks(8735));

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "MatchBets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System.Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "System.Admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 18, 0, 22, 639, DateTimeKind.Local).AddTicks(4130),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 846, DateTimeKind.Local).AddTicks(3418));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "FilterResults",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System.Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "System.Admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 18, 0, 22, 639, DateTimeKind.Local).AddTicks(3986),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 846, DateTimeKind.Local).AddTicks(3156));

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "FilterResults",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System.Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "System.Admin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 845, DateTimeKind.Local).AddTicks(8920),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 4, 18, 0, 22, 638, DateTimeKind.Local).AddTicks(8805));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "System.Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System.Admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 845, DateTimeKind.Local).AddTicks(8735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 4, 18, 0, 22, 638, DateTimeKind.Local).AddTicks(8659));

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "MatchBets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "System.Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System.Admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 846, DateTimeKind.Local).AddTicks(3418),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 4, 18, 0, 22, 639, DateTimeKind.Local).AddTicks(4130));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "FilterResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "System.Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System.Admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 4, 17, 54, 41, 846, DateTimeKind.Local).AddTicks(3156),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 4, 18, 0, 22, 639, DateTimeKind.Local).AddTicks(3986));

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "FilterResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "System.Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System.Admin");
        }
    }
}
