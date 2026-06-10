using MediatR;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteCandidateMatch
{
    public record DeleteCandidateMatchCommand(Guid CandidateId, Guid MatchId) : IRequest<ResultOf<bool>>;
}
