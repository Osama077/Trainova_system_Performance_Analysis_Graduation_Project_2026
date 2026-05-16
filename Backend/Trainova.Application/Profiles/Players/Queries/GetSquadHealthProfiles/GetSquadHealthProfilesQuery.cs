using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.Profiles.Players.Queries.GetSquadHealthProfiles
{
    public record GetSquadHealthProfilesQuery(
        Position? Position = null,
        InjuryStatus? InjuryStatus = null,
        SeverityGrade? SeverityGrade = null,
        string? SearchName = null) : IRequest<ResultOf<SquadHealthDetailes>>;

}
