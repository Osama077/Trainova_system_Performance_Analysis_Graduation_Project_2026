using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FitnessStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionMovements_Players_PlayerId",
                table: "SessionMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionMovements_TrainingSessions_TrainingSessionId",
                table: "SessionMovements");

            migrationBuilder.DropIndex(
                name: "IX_SessionMovements_PlayerId",
                table: "SessionMovements");

            migrationBuilder.DropIndex(
                name: "IX_SessionMovements_TrainingSessionId",
                table: "SessionMovements");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "SessionMovements");

            migrationBuilder.DropColumn(
                name: "IsSession",
                table: "AccessPolicies");

            migrationBuilder.RenameColumn(
                name: "TrainingSessionId",
                table: "SessionMovements",
                newName: "UserAccessPolicyId");

            migrationBuilder.RenameColumn(
                name: "SprintTest_Time30Meters",
                table: "CapacityTests",
                newName: "Time30Meters");

            migrationBuilder.RenameColumn(
                name: "SprintTest_Time10Meters",
                table: "CapacityTests",
                newName: "Time10Meters");

            migrationBuilder.RenameColumn(
                name: "ExplosivePowerTest_ReactiveStrengthIndex",
                table: "CapacityTests",
                newName: "ReactiveStrengthIndex");

            migrationBuilder.RenameColumn(
                name: "ExplosivePowerTest_CountermovementJumpHeight",
                table: "CapacityTests",
                newName: "CountermovementJumpHeight");

            migrationBuilder.RenameColumn(
                name: "AerobicCapacityTest_YoYoIntermittentRecoveryLevel2Distance",
                table: "CapacityTests",
                newName: "YoYoIntermittentRecoveryLevel2Distance");

            migrationBuilder.RenameColumn(
                name: "AerobicCapacityTest_YoYoIntermittentRecoveryLevel1Distance",
                table: "CapacityTests",
                newName: "YoYoIntermittentRecoveryLevel1Distance");

            migrationBuilder.RenameColumn(
                name: "AerobicCapacityTest_MaximumOxygenConsumption",
                table: "CapacityTests",
                newName: "MaximumOxygenConsumption");

            migrationBuilder.AlterColumn<decimal>(
                name: "Time30Meters",
                table: "CapacityTests",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Time10Meters",
                table: "CapacityTests",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReactiveStrengthIndex",
                table: "CapacityTests",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CountermovementJumpHeight",
                table: "CapacityTests",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaximumOxygenConsumption",
                table: "CapacityTests",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AccessPolicies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FitnessExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessExercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FitnessSessionExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: true),
                    Reps = table.Column<int>(type: "int", nullable: true),
                    Rounds = table.Column<int>(type: "int", nullable: true),
                    ActiveTimeSec = table.Column<int>(type: "int", nullable: true),
                    RestTimeSec = table.Column<int>(type: "int", nullable: true),
                    LoadDetails = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Intensity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessSessionExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FitnessSessionExercises_FitnessExercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "FitnessExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FitnessSessionExercises_TrainingSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "TrainingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionMovements_UserAccessPolicyId",
                table: "SessionMovements",
                column: "UserAccessPolicyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FitnessSessionExercises_ExerciseId",
                table: "FitnessSessionExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessSessionExercises_SessionId",
                table: "FitnessSessionExercises",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionMovements_UserAccessPolicies_UserAccessPolicyId",
                table: "SessionMovements",
                column: "UserAccessPolicyId",
                principalTable: "UserAccessPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionMovements_UserAccessPolicies_UserAccessPolicyId",
                table: "SessionMovements");

            migrationBuilder.DropTable(
                name: "FitnessSessionExercises");

            migrationBuilder.DropTable(
                name: "FitnessExercises");

            migrationBuilder.DropIndex(
                name: "IX_SessionMovements_UserAccessPolicyId",
                table: "SessionMovements");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AccessPolicies");

            migrationBuilder.RenameColumn(
                name: "UserAccessPolicyId",
                table: "SessionMovements",
                newName: "TrainingSessionId");

            migrationBuilder.RenameColumn(
                name: "YoYoIntermittentRecoveryLevel2Distance",
                table: "CapacityTests",
                newName: "AerobicCapacityTest_YoYoIntermittentRecoveryLevel2Distance");

            migrationBuilder.RenameColumn(
                name: "YoYoIntermittentRecoveryLevel1Distance",
                table: "CapacityTests",
                newName: "AerobicCapacityTest_YoYoIntermittentRecoveryLevel1Distance");

            migrationBuilder.RenameColumn(
                name: "Time30Meters",
                table: "CapacityTests",
                newName: "SprintTest_Time30Meters");

            migrationBuilder.RenameColumn(
                name: "Time10Meters",
                table: "CapacityTests",
                newName: "SprintTest_Time10Meters");

            migrationBuilder.RenameColumn(
                name: "ReactiveStrengthIndex",
                table: "CapacityTests",
                newName: "ExplosivePowerTest_ReactiveStrengthIndex");

            migrationBuilder.RenameColumn(
                name: "MaximumOxygenConsumption",
                table: "CapacityTests",
                newName: "AerobicCapacityTest_MaximumOxygenConsumption");

            migrationBuilder.RenameColumn(
                name: "CountermovementJumpHeight",
                table: "CapacityTests",
                newName: "ExplosivePowerTest_CountermovementJumpHeight");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "SessionMovements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "SprintTest_Time30Meters",
                table: "CapacityTests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "SprintTest_Time10Meters",
                table: "CapacityTests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExplosivePowerTest_ReactiveStrengthIndex",
                table: "CapacityTests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "AerobicCapacityTest_MaximumOxygenConsumption",
                table: "CapacityTests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExplosivePowerTest_CountermovementJumpHeight",
                table: "CapacityTests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddColumn<bool>(
                name: "IsSession",
                table: "AccessPolicies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SessionMovements_PlayerId",
                table: "SessionMovements",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionMovements_TrainingSessionId",
                table: "SessionMovements",
                column: "TrainingSessionId");

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
    }
}
