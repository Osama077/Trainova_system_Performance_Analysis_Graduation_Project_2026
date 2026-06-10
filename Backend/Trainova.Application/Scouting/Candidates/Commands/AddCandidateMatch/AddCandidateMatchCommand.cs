using MediatR;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.AddCandidateMatch
{
    public record AddCandidateMatchCommand(
        Guid CandidateId,
        DateTime MatchDate,
        string MatchName,
        int Goals,
        int Assists,
        float Rating,
        string? ScoutNotes) : IRequest<ResultOf<Guid>>;
}
