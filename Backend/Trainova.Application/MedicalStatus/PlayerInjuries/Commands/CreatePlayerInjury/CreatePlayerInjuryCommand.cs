using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury
{
    // Positional record representing the data required to create a PlayerInjury
    [Authorize(Roles = "Doctor")]
    public record CreatePlayerInjuryCommand(
        Guid InjuryId,
        Guid PlayerId,
        InjuryStatus Status,
        DateTime? HappendAt = null,
        InjuryCause Cause = default,
        SeverityGrade SevertiyGrade = default,
        BodyPart BodyPart = default,
        string? Notes = null,
        bool IsNew = false,
        DateTime? ExpectedReturnDate = null
    ) : IRequest<ResultOf<PlayerInjury>>;
}
