using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class FilterResultDefaultValuesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsShotOnTargetFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsShotFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPossesionFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCornerFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "HomeShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HomeShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HomePossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HomeCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayPossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AwayCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsShotOnTargetFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsShotFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPossesionFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCornerFound",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "HomeShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HomeShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HomePossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "HomeCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayShotOnTargetCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayShotCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayPossesion",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "AwayCornerCount",
                table: "FilterResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);
        }
    }
}
