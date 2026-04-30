using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.UpdateTrainingSession
{
    [Authorize(Role = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record UpdateTrainingSessionCommand(
        Guid Id,
        string? SessionName = null,
        string? Place = null,
        DateTime? WillHappenAt = null,
        PlanState? State = null)
        : IRequest<ResultOf<TrainingSession>>;
}
