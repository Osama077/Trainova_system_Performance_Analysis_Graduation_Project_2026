using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.FitnessStatus;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Application.FitnessStatus.Exercises.Commands.CreateExercise
{
    [Authorize(Roles = "FitnessCoach")]
    public record CreateExerciseCommand(
        string Name,
        string Code,
        ExerciseType Type,
        ExerciseCatagory Category,
        EquipmentRequired EquipmentRequired,
        MuscleGroup TargetMuscleGroups,
        ExerciseIntensity DefaultIntensity,
        int DefaultSets,
        string DefaultRepsOrDuration,
        int? DefaultRestBetweenSetsSec,
        string TypicalLoad,
        int? RecoveryTimeHours,
        string Description,
        string Contraindications) : IRequest<ResultOf<FitnessExercise>>;
}

