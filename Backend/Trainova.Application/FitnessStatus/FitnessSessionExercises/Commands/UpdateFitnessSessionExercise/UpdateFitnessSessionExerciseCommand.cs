using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.UpdateFitnessSessionExercise
{
    [Authorize(Roles = "FitnessCoach")]
    public record UpdateFitnessSessionExerciseCommand(
        Guid Id,
        ExerciseIntensity? Intensity = null,
        int? Sets = null,
        int? Reps = null,
        int? Rounds = null,
        int? ActiveTimeSec = null,
        int? RestTimeSec = null,
        string? LoadDetails = null) : IRequest<ResultOf<FitnessSessionExercise>>;
}
