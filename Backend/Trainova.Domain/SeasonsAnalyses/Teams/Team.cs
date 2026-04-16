using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.MatchsManagement.Events;
using Trainova.Domain.MatchsManagement.Lineups;
using Trainova.Domain.Profiles.ScoutingCandidates;
using Trainova.Domain.Profiles.TeamsStaff;

namespace Trainova.Domain.SeasonsAnalyses.Teams
{
    public class Team :AuditableEntity<Guid>
    {

        public string? TeamName { get; private set; }

        public string? Country { get; private set; }

        public ICollection<Event> Events { get; private set; } = new List<Event>();
        public ICollection<Lineup> Lineups { get; private set; } = new List<Lineup>();
        public ICollection<ScoutingCandidate> ScoutingCandidates { get; private set; } = new List<ScoutingCandidate>();
        private Team():base() { }

        public Team(
            string? teamName,
            string? country,
            Guid? createdBy = null) : base(Guid.NewGuid(),createdBy)
        {
            TeamName = teamName;
            Country = country;

        }

        public void Update(
            string? teamName = null,
            string? country = null)
        {
            MarkUpdatedNow();
            TeamName = teamName ?? TeamName;
            Country = country ?? Country;
        }
    }

}
