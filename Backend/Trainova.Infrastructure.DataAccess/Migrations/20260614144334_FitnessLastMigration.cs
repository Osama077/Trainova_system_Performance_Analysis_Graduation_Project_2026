using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FitnessLastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reps",
                table: "FitnessSessionExercises");

            migrationBuilder.DropColumn(
                name: "DefaultRepetitions",
                table: "FitnessExercises");

            migrationBuilder.RenameColumn(
                name: "ExerciseCatagory",
                table: "FitnessExercises",
                newName: "DefaultIntensity");

            migrationBuilder.RenameColumn(
                name: "DefaultExerciseIntensity",
                table: "FitnessExercises",
                newName: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "Sets",
                table: "FitnessSessionExercises",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepsOrDuration",
                table: "FitnessSessionExercises",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FitnessExercises",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Contraindications",
                table: "FitnessExercises",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DefaultRepsOrDuration",
                table: "FitnessExercises",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DefaultRestBetweenSetsSec",
                table: "FitnessExercises",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FitnessExercises",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RecoveryTimeHours",
                table: "FitnessExercises",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypicalLoad",
                table: "FitnessExercises",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepsOrDuration",
                table: "FitnessSessionExercises");

            migrationBuilder.DropColumn(
                name: "Contraindications",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "DefaultRepsOrDuration",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "DefaultRestBetweenSetsSec",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "RecoveryTimeHours",
                table: "FitnessExercises");

            migrationBuilder.DropColumn(
                name: "TypicalLoad",
                table: "FitnessExercises");

            migrationBuilder.RenameColumn(
                name: "DefaultIntensity",
                table: "FitnessExercises",
                newName: "ExerciseCatagory");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "FitnessExercises",
                newName: "DefaultExerciseIntensity");

            migrationBuilder.AlterColumn<int>(
                name: "Sets",
                table: "FitnessSessionExercises",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "FitnessSessionExercises",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FitnessExercises",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "DefaultRepetitions",
                table: "FitnessExercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
