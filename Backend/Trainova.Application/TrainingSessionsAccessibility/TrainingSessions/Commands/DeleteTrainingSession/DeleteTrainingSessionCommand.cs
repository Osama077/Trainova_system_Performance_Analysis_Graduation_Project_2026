using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.DeleteTrainingSession
{
    [Authorize(Role = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record DeleteTrainingSessionCommand(Guid Id)
        : IRequest<ResultOf<Done>>;
}
