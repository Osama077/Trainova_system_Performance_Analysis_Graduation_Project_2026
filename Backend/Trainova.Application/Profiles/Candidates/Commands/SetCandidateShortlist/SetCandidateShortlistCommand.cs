using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Candidates.Commands.SetCandidateShortlist
{
    public record SetCandidateShortlistCommand(Guid CandidateId, bool IsShortlisted) : IRequest<ResultOf<bool>>;
}
