using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    public partial class FullCornerAndFullWinLatestDecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Corner_Away_5_5_Over",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Corner_Home_5_5_Over",
                table: "FilterResults",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Corner_Away_5_5_Over",
                table: "FilterResults");

            migrationBuilder.DropColumn(
                name: "Corner_Home_5_5_Over",
                table: "FilterResults");
        }
    }
}
