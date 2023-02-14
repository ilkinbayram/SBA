using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class CreateIndexesForTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Country",
                table: "MatchBets",
                column: "Country")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_LeagueName",
                table: "MatchBets",
                column: "LeagueName")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_SerialUniqueID",
                table: "MatchBets",
                column: "SerialUniqueID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_SerialUniqueID",
                table: "FilterResults",
                column: "SerialUniqueID")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Country",
                table: "MatchBets");

            migrationBuilder.DropIndex(
                name: "IX_LeagueName",
                table: "MatchBets");

            migrationBuilder.DropIndex(
                name: "IX_SerialUniqueID",
                table: "MatchBets");

            migrationBuilder.DropIndex(
                name: "IX_SerialUniqueID",
                table: "FilterResults");
        }
    }
}
