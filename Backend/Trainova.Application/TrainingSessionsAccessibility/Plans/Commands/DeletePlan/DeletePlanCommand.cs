using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.DeletePlan
{
    [Authorize(Role = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record DeletePlanCommand(Guid Id)
        : IRequest<ResultOf<Done>>;
}
