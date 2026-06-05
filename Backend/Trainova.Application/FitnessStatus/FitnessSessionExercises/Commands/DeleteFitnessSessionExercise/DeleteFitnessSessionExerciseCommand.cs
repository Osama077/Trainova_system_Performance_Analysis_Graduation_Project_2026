using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.DeleteFitnessSessionExercise
{
    [Authorize(Roles = "FitnessCoach")]
    public record DeleteFitnessSessionExerciseCommand(Guid Id) : IRequest<ResultOf<Done>>;
}
