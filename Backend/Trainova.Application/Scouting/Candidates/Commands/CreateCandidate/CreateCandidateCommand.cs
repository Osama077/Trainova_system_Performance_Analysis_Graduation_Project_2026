using MediatR;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.CreateCandidate
{
    public record CreateCandidateCommand(
        string FullName,
        int Age,
        int Position,
        string? CurrentTeamName,
        string? Nationality = null,
        DateTime? ContractEnd = null,
        decimal? MarketValue = null,
        string? Agent = null,
        float ScoutRating = 0,
        int? ShortlistRank = null,
        int MatchesWatchedCount = 0,
        int Pace = 0,
        int Shooting = 0,
        int Dribbling = 0,
        int Passing = 0,
        int Physicality = 0,
        int Positioning = 0,
        int Defending = 0,
        int Vision = 0,
        DateTime? DateOfBirth = null,
        int? Height = null,
        int? Weight = null,
        string PreferredFoot = "Right") : IRequest<ResultOf<Guid>>;
}
