using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.UpdatePlayerInjury
{
    // Positional record representing the updatable fields for PlayerInjury
    [Authorize(Role = "Doctor")]
    public record UpdatePlayerInjuryCommand(
        Guid Id,
        InjuryStatus? Status,
        DateTime? HappendAt = null,
        InjuryCause? Cause = null,
        SevertiyGrade? SevertiyGrade = null,
        BodyPart? BodyPart = null,
        string? Notes = null,
        bool? IsNew = null,
        InjuryStatus? NewStatus = null,
        DateTime? ReturnedAt = null,
        DateTime? ExpectedReturnDate = null
    ) : IRequest<ResultOf<PlayerInjury>>;
}
