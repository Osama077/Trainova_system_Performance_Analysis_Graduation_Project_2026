using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScoutingCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScoutingCandidates_CurrentTeamId",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "CurrentMainPosition",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "CurrentTeamId",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "InjuryRisk",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "MedecalStatus",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "OtherAvailablePositions",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "PerformanceLevel",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "PerformanceScore",
                table: "ScoutingCandidates");

            migrationBuilder.AlterColumn<int>(
                name: "Vision",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Shooting",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Positioning",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Physicality",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Passing",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Pace",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "ScoutingCandidates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Dribbling",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Defending",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CurrentTeamName",
                table: "ScoutingCandidates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "ScoutingCandidates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "ScoutingCandidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredFoot",
                table: "ScoutingCandidates",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "ScoutingCandidates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CandidateMatch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MatchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    ScoutNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoutingCandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateMatch_ScoutingCandidates_ScoutingCandidateId",
                        column: x => x.ScoutingCandidateId,
                        principalTable: "ScoutingCandidates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScoutingCandidateNote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoutingCandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutingCandidateNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoutingCandidateNote_ScoutingCandidates_ScoutingCandidateId",
                        column: x => x.ScoutingCandidateId,
                        principalTable: "ScoutingCandidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoutingCandidates_CurrentTeamName",
                table: "ScoutingCandidates",
                column: "CurrentTeamName");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateMatch_ScoutingCandidateId",
                table: "CandidateMatch",
                column: "ScoutingCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoutingCandidateNote_ScoutingCandidateId",
                table: "ScoutingCandidateNote",
                column: "ScoutingCandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateMatch");

            migrationBuilder.DropTable(
                name: "ScoutingCandidateNote");

            migrationBuilder.DropIndex(
                name: "IX_ScoutingCandidates_CurrentTeamName",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "CurrentTeamName",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "PreferredFoot",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ScoutingCandidates");

            migrationBuilder.AlterColumn<int>(
                name: "Vision",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Shooting",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Positioning",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Physicality",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Passing",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Pace",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "ScoutingCandidates",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Dribbling",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Defending",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentMainPosition",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentTeamId",
                table: "ScoutingCandidates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "InjuryRisk",
                table: "ScoutingCandidates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "MedecalStatus",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAvailablePositions",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PerformanceLevel",
                table: "ScoutingCandidates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<float>(
                name: "PerformanceScore",
                table: "ScoutingCandidates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_ScoutingCandidates_CurrentTeamId",
                table: "ScoutingCandidates",
                column: "CurrentTeamId");
        }
    }
}
