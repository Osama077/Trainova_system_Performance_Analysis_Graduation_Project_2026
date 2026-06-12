using System;
using System.Collections.Generic;
using Trainova.Application.Scouting.Candidates.Commands.CreateCandidate;

namespace Trainova.Api.Requests.Scouting
{
    public class CreateCandidateRequest
    {
        // Basic Information
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? CurrentTeamName { get; set; }

        // Performance Information
        public float InjuryRisk { get; set; } // kept for backward compatibility in request body but not used in domain
        public decimal PerformanceLevel { get; set; } // kept for compatibility but not in command
        public float ScoutRating { get; set; }

        // Personal Details
        public DateTime? DateOfBirth { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string PreferredFoot { get; set; } = "Right";

        // Contract Information
        public string? Nationality { get; set; }
        public DateTime? ContractEnd { get; set; }
        public decimal? MarketValue { get; set; }
        public string? Agent { get; set; }

        // Skill Assessment
        public int Pace { get; set; }
        public int Shooting { get; set; }
        public int Dribbling { get; set; }
        public int Passing { get; set; }
        public int Physicality { get; set; }
        public int Positioning { get; set; }
        public int Defending { get; set; }
        public int Vision { get; set; }

        // Additional Information
        public int? ShortlistRank { get; set; }
        public int MatchesWatchedCount { get; set; }
        public int Position { get; set; }

        public CreateCandidateCommand ToCommand()
        {
            return new CreateCandidateCommand(
                FullName, 
                Age, 
                Position,
                CurrentTeamName,
                Nationality,
                ContractEnd,
                MarketValue,
                Agent,
                ScoutRating,
                ShortlistRank,
                MatchesWatchedCount,
                Pace,
                Shooting,
                Dribbling,
                Passing,
                Physicality,
                Positioning,
                Defending,
                Vision,
                DateOfBirth,
                Height,
                Weight,
                PreferredFoot);
        }
    }
}
