using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.FitnessStatus;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.UpdateFitnessSessionExercise
{
    [Authorize(Roles = "FitnessCoach")]
    public record UpdateFitnessSessionExerciseCommand(
        Guid Id,
        ExerciseIntensity? Intensity = null,
        int? Sets = null,
        string? RepsOrDuration = null,
        int? RestTimeSec = null,
        string? LoadDetails = null,
        int? Rounds = null,
        int? ActiveTimeSec = null) : IRequest<ResultOf<FitnessSessionExercise>>;
}
