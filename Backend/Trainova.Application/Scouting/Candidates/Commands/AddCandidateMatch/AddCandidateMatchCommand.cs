using MediatR;
using System;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.AddCandidateMatch
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record AddCandidateMatchCommand(
        Guid CandidateId,
        DateTime MatchDate,
        string MatchName,
        int Goals,
        int Assists,
        float Rating,
        string? ScoutNotes) : IRequest<ResultOf<Guid>>;
}
