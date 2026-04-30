using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession
{
    public record CreateTrainingSessionCommand(
        string SessionName,
        Guid? PolicyId,
        PlanState PlanState,
        string? Place,
        DateTime? WillHappenAt,
        Guid? PlanId,
        List<Guid> UserIds)
        : IRequest<ResultOf<TrainingSession>>;



}
