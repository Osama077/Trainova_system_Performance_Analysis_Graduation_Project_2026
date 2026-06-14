using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.FitnessStatus;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Application.FitnessStatus.Exercises.Commands.UpdateExercise
{
    [Authorize(Roles = "FitnessCoach")]
    public record UpdateExerciseCommand(
        Guid Id,
        string? Name = null,
        ExerciseType? Type = null,
        ExerciseCatagory? Category = null,
        EquipmentRequired? EquipmentRequired = null,
        MuscleGroup? TargetMuscleGroups = null,
        ExerciseIntensity? DefaultIntensity = null,
        int? DefaultSets = null,
        string? DefaultRepsOrDuration = null,
        int? DefaultRestBetweenSetsSec = null,
        string? Description = null,
        string? Contraindications = null) : IRequest<ResultOf<FitnessExercise>>;
}