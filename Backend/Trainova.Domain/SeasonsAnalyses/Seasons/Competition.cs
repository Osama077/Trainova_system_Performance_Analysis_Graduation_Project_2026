using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.MatchsManagement.Matches;

namespace Trainova.Domain.SeasonsAnalyses.Seasons
{
    public class Competition : AuditableEntity<Guid>
    {

        public string? CompetitionName { get; private set; }

        public Guid SeasonId { get; private set; }

        public string? SeasonName { get; private set; }

        public string? CountryName { get; private set; }



        private Competition() :base() { } // For ORM

        public Competition(
            string? competitionName,
            Guid seasonId,
            string? seasonName,
            string? countryName,
            Guid? createdBy = null):base(createdBy)
        {

            CompetitionName = competitionName;
            SeasonId = seasonId;
            SeasonName = seasonName;
            CountryName = countryName;
        }

        public void Update(
            string? competitionName = null,
            Guid? seasonId = null,
            string? seasonName = null,
            string? countryName = null)
        {
            MarkUpdatedNow();

            CompetitionName = competitionName ?? CompetitionName;
            SeasonId = seasonId ?? SeasonId;
            SeasonName = seasonName ?? SeasonName;
            CountryName = countryName ?? CountryName;
        }
    }

}
