using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init_3_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_UserAccessPolicies_UserAccessPolicyId",
                table: "TrainingSessions");

            migrationBuilder.RenameColumn(
                name: "UserAccessPolicyId",
                table: "TrainingSessions",
                newName: "AccessPolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSessions_UserAccessPolicyId",
                table: "TrainingSessions",
                newName: "IX_TrainingSessions_AccessPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_AccessPolicies_AccessPolicyId",
                table: "TrainingSessions",
                column: "AccessPolicyId",
                principalTable: "AccessPolicies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_AccessPolicies_AccessPolicyId",
                table: "TrainingSessions");

            migrationBuilder.RenameColumn(
                name: "AccessPolicyId",
                table: "TrainingSessions",
                newName: "UserAccessPolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSessions_AccessPolicyId",
                table: "TrainingSessions",
                newName: "IX_TrainingSessions_UserAccessPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_UserAccessPolicies_UserAccessPolicyId",
                table: "TrainingSessions",
                column: "UserAccessPolicyId",
                principalTable: "UserAccessPolicies",
                principalColumn: "Id");
        }
    }
}
