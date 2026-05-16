using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.UpdateTrainingSession
{
    [Authorize(Roles = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record UpdateTrainingSessionCommand(
        Guid Id,
        string? SessionName = null,
        string? Place = null,
        PlanState? PlanState = null,
        DateTime? WillHappenAt = null)
        : IRequest<ResultOf<TrainingSession>>;
}
