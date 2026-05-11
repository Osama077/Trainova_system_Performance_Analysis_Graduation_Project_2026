using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.UpdateInjury
{
    [Authorize(Role = "Doctor")]
    public record UpdateInjuryCommand(
        Guid Id,
        string? Name = null,
        string? Description = null,
        InjuryType? InjuryType = null,
        int? TimeAmountInDayes = null)
        : IRequest<ResultOf<Injury>>;

}
