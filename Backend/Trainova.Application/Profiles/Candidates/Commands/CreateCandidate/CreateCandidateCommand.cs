using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.Profiles;

namespace Trainova.Application.Profiles.Candidates.Commands.CreateCandidate
{
    public record CreateCandidateCommand(
        string FullName,
        DateTime ScoutedAt,
        string? Email)
        : IRequest<ResultOf<Candidate>>;
}
