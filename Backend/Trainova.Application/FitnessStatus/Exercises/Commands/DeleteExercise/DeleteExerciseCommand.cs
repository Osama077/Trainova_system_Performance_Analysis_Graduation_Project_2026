using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.Exercises.Commands.DeleteExercise
{
    [Authorize(Roles = "FitnessCoach")]
    public record DeleteExerciseCommand(Guid Id) : IRequest<ResultOf<Done>>;
}
