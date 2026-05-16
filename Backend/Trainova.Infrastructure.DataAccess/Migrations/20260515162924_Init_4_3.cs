using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init_4_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccessPolicies_TrainingSessions_TrainingSessionId",
                table: "UserAccessPolicies");

            migrationBuilder.DropIndex(
                name: "IX_UserAccessPolicies_TrainingSessionId",
                table: "UserAccessPolicies");

            migrationBuilder.DropColumn(
                name: "TrainingSessionId",
                table: "UserAccessPolicies");

            migrationBuilder.AlterColumn<string>(
                name: "TrainingSessionName",
                table: "TrainingSessions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "TrainingSessions",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionType",
                table: "TrainingSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionType",
                table: "TrainingSessions");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingSessionId",
                table: "UserAccessPolicies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TrainingSessionName",
                table: "TrainingSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "TrainingSessions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessPolicies_TrainingSessionId",
                table: "UserAccessPolicies",
                column: "TrainingSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccessPolicies_TrainingSessions_TrainingSessionId",
                table: "UserAccessPolicies",
                column: "TrainingSessionId",
                principalTable: "TrainingSessions",
                principalColumn: "Id");
        }
    }
}
