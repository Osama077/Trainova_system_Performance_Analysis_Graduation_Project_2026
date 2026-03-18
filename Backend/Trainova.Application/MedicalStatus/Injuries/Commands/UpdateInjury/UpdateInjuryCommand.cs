using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.UpdateInjury
{
    public record UpdateInjuryCommand(
        Guid Id,
        string? Name = null,
        string? Description = null,
        string? InjuryType = null,
        string? TimeType = null,
        decimal? TimeAmount = null)
        : IRequest<ResultOf<Injury>>;



}
