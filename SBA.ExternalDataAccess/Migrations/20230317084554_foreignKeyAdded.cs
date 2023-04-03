using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class foreignKeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AverageStatisticsHolders_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropIndex(
                name: "IX_AverageStatisticsHolders_LeagueStatisticsHolderId",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "LeagueStatisticsHolderId",
                table: "AverageStatisticsHolders");

            migrationBuilder.CreateIndex(
                name: "IX_AverageStatisticsHolders_LeagueStaisticsHolderId",
                table: "AverageStatisticsHolders",
                column: "LeagueStaisticsHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AverageStatisticsHolders_LeagueStatisticsHolders_LeagueStaisticsHolderId",
                table: "AverageStatisticsHolders",
                column: "LeagueStaisticsHolderId",
                principalTable: "LeagueStatisticsHolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AverageStatisticsHolders_LeagueStatisticsHolders_LeagueStaisticsHolderId",
                table: "AverageStatisticsHolders");

            migrationBuilder.DropIndex(
                name: "IX_AverageStatisticsHolders_LeagueStaisticsHolderId",
                table: "AverageStatisticsHolders");

            migrationBuilder.AddColumn<int>(
                name: "LeagueStatisticsHolderId",
                table: "AverageStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AverageStatisticsHolders_LeagueStatisticsHolderId",
                table: "AverageStatisticsHolders",
                column: "LeagueStatisticsHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AverageStatisticsHolders_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                table: "AverageStatisticsHolders",
                column: "LeagueStatisticsHolderId",
                principalTable: "LeagueStatisticsHolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
