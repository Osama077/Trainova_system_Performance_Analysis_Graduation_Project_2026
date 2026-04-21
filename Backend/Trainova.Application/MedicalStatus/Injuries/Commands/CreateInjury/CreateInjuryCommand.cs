using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.CreateInjury
{
    [Authorize(Role ="Doctor")]
    public record CreateInjuryCommand(
        string Name,
        string Description,
        string InjuryType,
        string TimeType,
        decimal TimeAmount)
        : IRequest<ResultOf<Injury>>;

}