using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class decimalPrecisionUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_HomeTeam",
                table: "StatisticsHolder",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_AwayTeam",
                table: "StatisticsHolder",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_HomeTeam",
                table: "StatisticsHolder",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_AwayTeam",
                table: "StatisticsHolder",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_HomeTeam",
                table: "StatisticsHolder",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_AwayTeam",
                table: "StatisticsHolder",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_HomeTeam",
                table: "StatisticsHolder",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_AwayTeam",
                table: "StatisticsHolder",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "SH_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HT_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_HomeTeam",
                table: "StatisticsHolder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_SH_Goals_AwayTeam",
                table: "StatisticsHolder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_HomeTeam",
                table: "StatisticsHolder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_HT_Goals_AwayTeam",
                table: "StatisticsHolder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_HomeTeam",
                table: "StatisticsHolder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Goals_AwayTeam",
                table: "StatisticsHolder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_HomeTeam",
                table: "StatisticsHolder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Average_FT_Corners_AwayTeam",
                table: "StatisticsHolder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: -1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "SH_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "HT_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "FT_GoalsAverage",
                table: "LeagueStatisticsHolders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldDefaultValue: -1m);
        }
    }
}
