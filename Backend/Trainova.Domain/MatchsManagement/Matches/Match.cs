using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.MatchsManagement.Matches
{

    public class Match : AuditableEntity<Guid>
    {

        public Guid CompetitionId { get; private set; }

        public DateOnly MatchDate { get; private set; }

        public Guid HomeTeamId { get; private set; }

        public Guid AwayTeamId { get; private set; }

        public byte HomeScore { get; private set; }

        public byte AwayScore { get; private set; }

        public string Stadium { get; private set; }

        public string Referee { get; private set; }

        public int MatchWeek { get; private set; }



        private Match() :base() { } // For ORM

        public Match(
            Guid competitionId,
            DateOnly matchDate,
            Guid homeTeamId,
            Guid awayTeamId,
            string stadium,
            string referee,
            int matchWeek,
            Guid? createdBy = null) : base(createdBy)
        {
            Id = Guid.NewGuid();
            CompetitionId = competitionId;
            MatchDate = matchDate;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            Stadium = stadium;
            Referee = referee;
            MatchWeek = matchWeek;

            HomeScore = 0;
            AwayScore = 0;

        }

        public void Update(
            Guid? competitionId = null,
            DateOnly? matchDate = null,
            Guid? homeTeamId = null,
            Guid? awayTeamId = null,
            byte? homeScore = null,
            byte? awayScore = null,
            string? stadium = null,
            string? referee = null,
            int? matchWeek = null)
        {

            CompetitionId = competitionId ?? CompetitionId;
            MatchDate = matchDate ?? MatchDate;
            HomeTeamId = homeTeamId ?? HomeTeamId;
            AwayTeamId = awayTeamId ?? AwayTeamId;
            HomeScore = homeScore ?? HomeScore;
            AwayScore = awayScore ?? AwayScore;
            Stadium = stadium ?? Stadium;
            Referee = referee ?? Referee;
            MatchWeek = matchWeek ?? MatchWeek;
            MarkUpdatedNow();

        }

        public void UpdateScore(byte homeScore, byte awayScore)
        {
            HomeScore = homeScore;
            AwayScore = awayScore;
        }
    }


}
