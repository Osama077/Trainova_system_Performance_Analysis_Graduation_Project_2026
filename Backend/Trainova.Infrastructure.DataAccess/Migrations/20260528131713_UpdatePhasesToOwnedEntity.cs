using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhasesToOwnedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanPhases_PlayerInjuries_PlayerInjuryId",
                table: "PlanPhases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanPhases",
                table: "PlanPhases");

            migrationBuilder.RenameTable(
                name: "PlanPhases",
                newName: "RecoveryPlanPhases");

            migrationBuilder.RenameIndex(
                name: "IX_PlanPhases_PlayerInjuryId_Order",
                table: "RecoveryPlanPhases",
                newName: "IX_RecoveryPlanPhases_PlayerInjuryId_Order");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RecoveryPlanPhases",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RecoveryPlanPhases",
                type: "nvarchar(1400)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1200)",
                oldMaxLength: 1200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecoveryPlanPhases",
                table: "RecoveryPlanPhases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecoveryPlanPhases_PlayerInjuries_PlayerInjuryId",
                table: "RecoveryPlanPhases",
                column: "PlayerInjuryId",
                principalTable: "PlayerInjuries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecoveryPlanPhases_PlayerInjuries_PlayerInjuryId",
                table: "RecoveryPlanPhases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecoveryPlanPhases",
                table: "RecoveryPlanPhases");

            migrationBuilder.RenameTable(
                name: "RecoveryPlanPhases",
                newName: "PlanPhases");

            migrationBuilder.RenameIndex(
                name: "IX_RecoveryPlanPhases_PlayerInjuryId_Order",
                table: "PlanPhases",
                newName: "IX_PlanPhases_PlayerInjuryId_Order");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PlanPhases",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PlanPhases",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanPhases",
                table: "PlanPhases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanPhases_PlayerInjuries_PlayerInjuryId",
                table: "PlanPhases",
                column: "PlayerInjuryId",
                principalTable: "PlayerInjuries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
