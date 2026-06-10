using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddScoutingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateMatch_ScoutingCandidates_ScoutingCandidateId",
                table: "CandidateMatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateMatch",
                table: "CandidateMatch");

            migrationBuilder.DropIndex(
                name: "IX_CandidateMatch_ScoutingCandidateId",
                table: "CandidateMatch");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CandidateMatch");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CandidateMatch");

            migrationBuilder.DropColumn(
                name: "ScoutingCandidateId",
                table: "CandidateMatch");

            migrationBuilder.RenameTable(
                name: "CandidateMatch",
                newName: "ScoutingCandidateMatch");

            migrationBuilder.AlterColumn<string>(
                name: "ScoutNotes",
                table: "ScoutingCandidateMatch",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MatchName",
                table: "ScoutingCandidateMatch",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoutingCandidateMatch",
                table: "ScoutingCandidateMatch",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ScoutingCandidateSeasonStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Season = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    League = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Matches = table.Column<int>(type: "int", nullable: false),
                    PassAccuracy = table.Column<float>(type: "real", nullable: false),
                    ShotsPer90 = table.Column<float>(type: "real", nullable: false),
                    XgPer90 = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutingCandidateSeasonStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoutingCandidateSeasonStatistics_ScoutingCandidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "ScoutingCandidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoutingCandidateMatch_CandidateId",
                table: "ScoutingCandidateMatch",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoutingCandidateSeasonStatistics_CandidateId",
                table: "ScoutingCandidateSeasonStatistics",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutingCandidateMatch_ScoutingCandidates_CandidateId",
                table: "ScoutingCandidateMatch",
                column: "CandidateId",
                principalTable: "ScoutingCandidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoutingCandidateMatch_ScoutingCandidates_CandidateId",
                table: "ScoutingCandidateMatch");

            migrationBuilder.DropTable(
                name: "ScoutingCandidateSeasonStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoutingCandidateMatch",
                table: "ScoutingCandidateMatch");

            migrationBuilder.DropIndex(
                name: "IX_ScoutingCandidateMatch_CandidateId",
                table: "ScoutingCandidateMatch");

            migrationBuilder.RenameTable(
                name: "ScoutingCandidateMatch",
                newName: "CandidateMatch");

            migrationBuilder.AlterColumn<string>(
                name: "ScoutNotes",
                table: "CandidateMatch",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1200)",
                oldMaxLength: 1200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MatchName",
                table: "CandidateMatch",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CandidateMatch",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "CandidateMatch",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ScoutingCandidateId",
                table: "CandidateMatch",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateMatch",
                table: "CandidateMatch",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateMatch_ScoutingCandidateId",
                table: "CandidateMatch",
                column: "ScoutingCandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateMatch_ScoutingCandidates_ScoutingCandidateId",
                table: "CandidateMatch",
                column: "ScoutingCandidateId",
                principalTable: "ScoutingCandidates",
                principalColumn: "Id");
        }
    }
}
