using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainovaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Agent",
                table: "ScoutingCandidates",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEnd",
                table: "ScoutingCandidates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Defending",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dribbling",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MarketValue",
                table: "ScoutingCandidates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatchesWatchedCount",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "ScoutingCandidates",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ScoutingCandidates",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pace",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Passing",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Physicality",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Positioning",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ScoutRating",
                table: "ScoutingCandidates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Shooting",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShortlistRank",
                table: "ScoutingCandidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vision",
                table: "ScoutingCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agent",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "ContractEnd",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Defending",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Dribbling",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "MarketValue",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "MatchesWatchedCount",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Pace",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Passing",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Physicality",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Positioning",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "ScoutRating",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Shooting",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "ShortlistRank",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ScoutingCandidates");

            migrationBuilder.DropColumn(
                name: "Vision",
                table: "ScoutingCandidates");
        }
    }
}
