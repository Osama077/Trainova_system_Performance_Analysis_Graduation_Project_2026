using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ScoutingDataFormUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultExerciseIntensity",
                table: "FitnessExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DefaultRepetitions",
                table: "FitnessExercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DefaultSets",
                table: "FitnessExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentRequired",
                table: "FitnessExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseCatagory",
                table: "FitnessExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetMuscleGroup",
                table: "FitnessExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultExerciseIntensity",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "DefaultRepetitions",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "DefaultSets",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "EquipmentRequired",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "ExerciseCatagory",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "TargetMuscleGroup",
                table: "FitnessExercises");
        }
    }
}
