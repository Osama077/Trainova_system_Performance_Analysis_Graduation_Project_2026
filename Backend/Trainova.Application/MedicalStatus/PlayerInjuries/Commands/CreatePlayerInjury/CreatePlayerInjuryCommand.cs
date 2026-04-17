using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury
{
    // Positional record representing the data required to create a PlayerInjury
    public record CreatePlayerInjuryCommand(
        Guid InjuryId,
        Guid PlayerId,
        InjuryStatus Status,
        DateTime? HappendAt = null,
        InjuryCause Cause = default,
        SevertiyGrade SevertiyGrade = default,
        string? BodyPart = null,
        string? Notes = null,
        bool IsNew = false
    ) : IRequest<ResultOf<PlayerInjury>>;
}
