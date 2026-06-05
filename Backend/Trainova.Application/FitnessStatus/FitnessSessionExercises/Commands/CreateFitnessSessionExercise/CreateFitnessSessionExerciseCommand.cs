using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.CreateFitnessSessionExercise
{
    [Authorize(Roles = "FitnessCoach")]
    public record CreateFitnessSessionExerciseCommand(
        Guid SessionId,
        Guid ExerciseId,
        ExerciseIntensity Intensity,
        int? Sets = null,
        int? Reps = null,
        int? Rounds = null,
        int? ActiveTimeSec = null,
        int? RestTimeSec = null,
        string? LoadDetails = null) : IRequest<ResultOf<FitnessSessionExercise>>;
}
