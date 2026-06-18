using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession
{
    [Authorize(Roles = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record CreateTrainingSessionCommand(
        string SessionName,
        Guid? PolicyId,
        PlanState PlanState,
        string? Place,
        DateTime? WillHappenAt,
        List<Guid> UserIds)
        : IRequest<ResultOf<TrainingSession>>;



}
