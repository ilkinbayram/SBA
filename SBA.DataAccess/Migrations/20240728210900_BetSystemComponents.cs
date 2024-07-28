using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BetSystemComponents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 791, DateTimeKind.Local).AddTicks(4087),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(3659));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 791, DateTimeKind.Local).AddTicks(3757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(3487));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(2789),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 631, DateTimeKind.Local).AddTicks(2518));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 792, DateTimeKind.Local).AddTicks(3071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(8822));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 792, DateTimeKind.Local).AddTicks(2674),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(8695));

            migrationBuilder.CreateTable(
                name: "BetSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "BetSystem-B15E"),
                    AcceptedOdd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    AcceptedDivider = table.Column<int>(type: "int", nullable: false),
                    StartingAmount = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    StepsGoalCount = table.Column<int>(type: "int", nullable: false),
                    MaxBundleCount = table.Column<int>(type: "int", nullable: false),
                    ModelType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(5659)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(6062)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchIdentifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serial = table.Column<int>(type: "int", nullable: false),
                    HomeTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HT_Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FT_Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchIdentifier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ChildId = table.Column<int>(type: "int", nullable: true),
                    BetSystemId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    InsuredBetAmount = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    TotalBalance = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    ModelType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(388)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(752)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_BetSystems_BetSystemId",
                        column: x => x.BetSystemId,
                        principalTable: "BetSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forecast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serial = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    Is99Percent = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchIdentifierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forecast_MatchIdentifier_MatchIdentifierId",
                        column: x => x.MatchIdentifierId,
                        principalTable: "MatchIdentifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepId = table.Column<int>(type: "int", nullable: false),
                    ModelType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(8275)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(8687)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bundles_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartingStepId = table.Column<int>(type: "int", nullable: false),
                    TotalBalance = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    ModelType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(5307)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 794, DateTimeKind.Local).AddTicks(5721)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedSteps_Steps_StartingStepId",
                        column: x => x.StartingStepId,
                        principalTable: "Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Predictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForecastId = table.Column<int>(type: "int", nullable: false),
                    Odd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    ProbableWinPercent = table.Column<int>(type: "int", nullable: false),
                    AnalysingProbableMatchCount = table.Column<int>(type: "int", nullable: false),
                    ModelType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(7814)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(8215)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predictions_Forecast_ForecastId",
                        column: x => x.ForecastId,
                        principalTable: "Forecast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboBets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalOdd = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    PotentialWinningPercent = table.Column<int>(type: "int", nullable: false),
                    IsInsuredBet = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BetType = table.Column<int>(type: "int", nullable: false),
                    BundleId = table.Column<int>(type: "int", nullable: false),
                    ModelType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(2887)),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 795, DateTimeKind.Local).AddTicks(3249)),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System.Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboBets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComboBets_Bundles_BundleId",
                        column: x => x.BundleId,
                        principalTable: "Bundles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboBetPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComboBetId = table.Column<int>(type: "int", nullable: false),
                    PredictionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboBetPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComboBetPredictions_ComboBets_ComboBetId",
                        column: x => x.ComboBetId,
                        principalTable: "ComboBets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboBetPredictions_Predictions_PredictionId",
                        column: x => x.PredictionId,
                        principalTable: "Predictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bundles_StepId",
                table: "Bundles",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboBetPredictions_ComboBetId",
                table: "ComboBetPredictions",
                column: "ComboBetId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboBetPredictions_PredictionId",
                table: "ComboBetPredictions",
                column: "PredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboBets_BundleId",
                table: "ComboBets",
                column: "BundleId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecast_MatchIdentifierId",
                table: "Forecast",
                column: "MatchIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_ForecastId",
                table: "Predictions",
                column: "ForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedSteps_StartingStepId",
                table: "SavedSteps",
                column: "StartingStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_BetSystemId",
                table: "Steps",
                column: "BetSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboBetPredictions");

            migrationBuilder.DropTable(
                name: "SavedSteps");

            migrationBuilder.DropTable(
                name: "ComboBets");

            migrationBuilder.DropTable(
                name: "Predictions");

            migrationBuilder.DropTable(
                name: "Bundles");

            migrationBuilder.DropTable(
                name: "Forecast");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "MatchIdentifier");

            migrationBuilder.DropTable(
                name: "BetSystems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PerformanceOverall",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(3659),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 791, DateTimeKind.Local).AddTicks(4087));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "MatchBets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(3487),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 791, DateTimeKind.Local).AddTicks(3757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 631, DateTimeKind.Local).AddTicks(2518),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 793, DateTimeKind.Local).AddTicks(2789));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(8822),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 792, DateTimeKind.Local).AddTicks(3071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "FilterResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 6, 45, 57, 630, DateTimeKind.Local).AddTicks(8695),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 1, 8, 58, 792, DateTimeKind.Local).AddTicks(2674));
        }
    }
}
