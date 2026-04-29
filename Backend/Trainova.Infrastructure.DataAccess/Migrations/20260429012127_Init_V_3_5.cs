using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init_V_3_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputedFeatures_Matches_MatchId",
                table: "ComputedFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputedFeatures_Players_PlayerId",
                table: "ComputedFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Matches_MatchId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Teams_TeamId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Matches_MatchId",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_PlayerId",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Teams_TeamId",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Competitions_CompetitionId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_AwayTeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchVideos_Matches_RelatedMatchId",
                table: "MatchVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_ModelScores_Matches_MatchId",
                table: "ModelScores");

            migrationBuilder.DropForeignKey(
                name: "FK_ModelScores_Players_PlayerId",
                table: "ModelScores");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoutingCandidates_Teams_CurrentTeamId",
                table: "ScoutingCandidates");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionMovement_Players_PlayerId",
                table: "SessionMovement");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionMovement_TrainingSessions_TrainingSessionId",
                table: "SessionMovement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionMovement",
                table: "SessionMovement");

            migrationBuilder.RenameTable(
                name: "SessionMovement",
                newName: "SessionMovements");

            migrationBuilder.RenameIndex(
                name: "IX_SessionMovement_TrainingSessionId",
                table: "SessionMovements",
                newName: "IX_SessionMovements_TrainingSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionMovement_PlayerId",
                table: "SessionMovements",
                newName: "IX_SessionMovements_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionMovements",
                table: "SessionMovements",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CapacityTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AerobicCapacityTest_MaximumOxygenConsumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AerobicCapacityTest_YoYoIntermittentRecoveryLevel1Distance = table.Column<int>(type: "int", nullable: false),
                    AerobicCapacityTest_YoYoIntermittentRecoveryLevel2Distance = table.Column<int>(type: "int", nullable: false),
                    SprintTest_Time10Meters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SprintTest_Time30Meters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExplosivePowerTest_CountermovementJumpHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExplosivePowerTest_ReactiveStrengthIndex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacityTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecoveryPlanPhases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerInjuryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecoveryPlanPhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecoveryPlanPhases_PlayerInjuries_PlayerInjuryId",
                        column: x => x.PlayerInjuryId,
                        principalTable: "PlayerInjuries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryPlanPhases_PlayerInjuryId",
                table: "RecoveryPlanPhases",
                column: "PlayerInjuryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionMovements_Players_PlayerId",
                table: "SessionMovements",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionMovements_TrainingSessions_TrainingSessionId",
                table: "SessionMovements",
                column: "TrainingSessionId",
                principalTable: "TrainingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionMovements_Players_PlayerId",
                table: "SessionMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionMovements_TrainingSessions_TrainingSessionId",
                table: "SessionMovements");

            migrationBuilder.DropTable(
                name: "CapacityTests");

            migrationBuilder.DropTable(
                name: "RecoveryPlanPhases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionMovements",
                table: "SessionMovements");

            migrationBuilder.RenameTable(
                name: "SessionMovements",
                newName: "SessionMovement");

            migrationBuilder.RenameIndex(
                name: "IX_SessionMovements_TrainingSessionId",
                table: "SessionMovement",
                newName: "IX_SessionMovement_TrainingSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionMovements_PlayerId",
                table: "SessionMovement",
                newName: "IX_SessionMovement_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionMovement",
                table: "SessionMovement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComputedFeatures_Matches_MatchId",
                table: "ComputedFeatures",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputedFeatures_Players_PlayerId",
                table: "ComputedFeatures",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Matches_MatchId",
                table: "Events",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Teams_TeamId",
                table: "Events",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Matches_MatchId",
                table: "Lineups",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_PlayerId",
                table: "Lineups",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Teams_TeamId",
                table: "Lineups",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Competitions_CompetitionId",
                table: "Matches",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchVideos_Matches_RelatedMatchId",
                table: "MatchVideos",
                column: "RelatedMatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModelScores_Matches_MatchId",
                table: "ModelScores",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModelScores_Players_PlayerId",
                table: "ModelScores",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutingCandidates_Teams_CurrentTeamId",
                table: "ScoutingCandidates",
                column: "CurrentTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionMovement_Players_PlayerId",
                table: "SessionMovement",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionMovement_TrainingSessions_TrainingSessionId",
                table: "SessionMovement",
                column: "TrainingSessionId",
                principalTable: "TrainingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
