using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SesionSpeedIndexAndPlayerCapacityTestEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerLoad",
                table: "SessionMovements");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "SessionMovements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "FootageLoadToCapacityRatio",
                table: "SessionMovements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "FootageStatus",
                table: "SessionMovements",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "LoadRatioFromLastSession",
                table: "SessionMovements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OverriddenLoad",
                table: "SessionMovements",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PlayerCalculatedLoad",
                table: "SessionMovements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CalculatedCapacity",
                table: "CapacityTests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CreationType",
                table: "CapacityTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "OverriddenCapacity",
                table: "CapacityTests",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProgressFromLastTest",
                table: "CapacityTests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "SessionMovements");

            migrationBuilder.DropColumn(
                name: "FootageLoadToCapacityRatio",
                table: "SessionMovements");

            migrationBuilder.DropColumn(
                name: "FootageStatus",
                table: "SessionMovements");

            migrationBuilder.DropColumn(
                name: "LoadRatioFromLastSession",
                table: "SessionMovements");

            migrationBuilder.DropColumn(
                name: "OverriddenLoad",
                table: "SessionMovements");

            migrationBuilder.DropColumn(
                name: "PlayerCalculatedLoad",
                table: "SessionMovements");

            migrationBuilder.DropColumn(
                name: "CalculatedCapacity",
                table: "CapacityTests");

            migrationBuilder.DropColumn(
                name: "CreationType",
                table: "CapacityTests");

            migrationBuilder.DropColumn(
                name: "OverriddenCapacity",
                table: "CapacityTests");

            migrationBuilder.DropColumn(
                name: "ProgressFromLastTest",
                table: "CapacityTests");

            migrationBuilder.AddColumn<decimal>(
                name: "PlayerLoad",
                table: "SessionMovements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);
        }
    }
}
