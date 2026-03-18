using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.CreateInjury
{
    public record CreateInjuryCommand(
        string Name,
        string Description,
        string InjuryType,
        string TimeType,
        decimal TimeAmount)
        : IRequest<ResultOf<Injury>>;

}