using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury
{
    // Positional record representing the data required to create a PlayerInjury
    [Authorize(Role = "Doctor")]
    public record CreatePlayerInjuryCommand(
        Guid InjuryId,
        Guid PlayerId,
        InjuryStatus Status,
        DateTime? HappendAt = null,
        InjuryCause Cause = default,
        SevertiyGrade SevertiyGrade = default,
        BodyPart BodyPart = default,
        string? Notes = null,
        bool IsNew = false,
        DateTime? ExpectedReturnDate = null
    ) : IRequest<ResultOf<PlayerInjury>>;
}
