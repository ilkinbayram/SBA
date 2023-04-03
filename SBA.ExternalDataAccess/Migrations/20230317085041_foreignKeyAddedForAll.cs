using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    public partial class foreignKeyAddedForAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparisonStatisticsHolder_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                table: "ComparisonStatisticsHolder");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPerformanceStatisticsHolders_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                table: "TeamPerformanceStatisticsHolders");

            migrationBuilder.DropIndex(
                name: "IX_TeamPerformanceStatisticsHolders_LeagueStatisticsHolderId",
                table: "TeamPerformanceStatisticsHolders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComparisonStatisticsHolder",
                table: "ComparisonStatisticsHolder");

            migrationBuilder.DropIndex(
                name: "IX_ComparisonStatisticsHolder_LeagueStatisticsHolderId",
                table: "ComparisonStatisticsHolder");

            migrationBuilder.DropColumn(
                name: "LeagueStatisticsHolderId",
                table: "TeamPerformanceStatisticsHolders");

            migrationBuilder.DropColumn(
                name: "LeagueStatisticsHolderId",
                table: "ComparisonStatisticsHolder");

            migrationBuilder.RenameTable(
                name: "ComparisonStatisticsHolder",
                newName: "ComparisonStatisticsHolders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComparisonStatisticsHolders",
                table: "ComparisonStatisticsHolders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPerformanceStatisticsHolders_LeagueStaisticsHolderId",
                table: "TeamPerformanceStatisticsHolders",
                column: "LeagueStaisticsHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonStatisticsHolders_LeagueStaisticsHolderId",
                table: "ComparisonStatisticsHolders",
                column: "LeagueStaisticsHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparisonStatisticsHolders_LeagueStatisticsHolders_LeagueStaisticsHolderId",
                table: "ComparisonStatisticsHolders",
                column: "LeagueStaisticsHolderId",
                principalTable: "LeagueStatisticsHolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPerformanceStatisticsHolders_LeagueStatisticsHolders_LeagueStaisticsHolderId",
                table: "TeamPerformanceStatisticsHolders",
                column: "LeagueStaisticsHolderId",
                principalTable: "LeagueStatisticsHolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparisonStatisticsHolders_LeagueStatisticsHolders_LeagueStaisticsHolderId",
                table: "ComparisonStatisticsHolders");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPerformanceStatisticsHolders_LeagueStatisticsHolders_LeagueStaisticsHolderId",
                table: "TeamPerformanceStatisticsHolders");

            migrationBuilder.DropIndex(
                name: "IX_TeamPerformanceStatisticsHolders_LeagueStaisticsHolderId",
                table: "TeamPerformanceStatisticsHolders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComparisonStatisticsHolders",
                table: "ComparisonStatisticsHolders");

            migrationBuilder.DropIndex(
                name: "IX_ComparisonStatisticsHolders_LeagueStaisticsHolderId",
                table: "ComparisonStatisticsHolders");

            migrationBuilder.RenameTable(
                name: "ComparisonStatisticsHolders",
                newName: "ComparisonStatisticsHolder");

            migrationBuilder.AddColumn<int>(
                name: "LeagueStatisticsHolderId",
                table: "TeamPerformanceStatisticsHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeagueStatisticsHolderId",
                table: "ComparisonStatisticsHolder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComparisonStatisticsHolder",
                table: "ComparisonStatisticsHolder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPerformanceStatisticsHolders_LeagueStatisticsHolderId",
                table: "TeamPerformanceStatisticsHolders",
                column: "LeagueStatisticsHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonStatisticsHolder_LeagueStatisticsHolderId",
                table: "ComparisonStatisticsHolder",
                column: "LeagueStatisticsHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparisonStatisticsHolder_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                table: "ComparisonStatisticsHolder",
                column: "LeagueStatisticsHolderId",
                principalTable: "LeagueStatisticsHolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPerformanceStatisticsHolders_LeagueStatisticsHolders_LeagueStatisticsHolderId",
                table: "TeamPerformanceStatisticsHolders",
                column: "LeagueStatisticsHolderId",
                principalTable: "LeagueStatisticsHolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
