using System.Text.RegularExpressions;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Profiles.Players;
using Trainova.Domain.SeasonsAnalyses.Teams;

namespace Trainova.Domain.MatchsManagement.Lineups
{
    public class Lineup : AuditableEntity<Guid>
    {
        public Guid MatchId { get; private set; }
        public Match Match { get; private set; }

        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }

        public Guid TeamId { get; private set; }
        public Team Team { get; private set; }

        public Position? StartingPosition { get; private set; }

        public bool IsStarter { get; private set; }

        public int MinutesPlayed { get; private set; }


        private Lineup() : base()
        {
        }

        public Lineup(
            Guid matchId,
            Guid playerId,
            Guid teamId,
            Position? startingPosition,
            bool isStarter,
            int minutesPlayed,
            Guid? createdBy = null) : base(createdBy)
        {
            MatchId = matchId;
            PlayerId = playerId;
            TeamId = teamId;
            StartingPosition = startingPosition;
            IsStarter = isStarter;
            MinutesPlayed = minutesPlayed;
            LastUpdate = DateTime.UtcNow;
        }

        public void Update(
            Guid? matchId = null,
            Guid? playerId = null,
            Guid? teamId = null,
            Position? startingPosition = null,
            bool? isStarter = null,
            int? minutesPlayed = null)
        {
            if (matchId.HasValue)
                MatchId = matchId.Value;

            if (playerId.HasValue)
                PlayerId = playerId.Value;

            if (teamId.HasValue)
                TeamId = teamId.Value;

            if (startingPosition.HasValue)
                StartingPosition = startingPosition;

            if (isStarter.HasValue)
                IsStarter = isStarter.Value;

            if (minutesPlayed.HasValue)
                MinutesPlayed = minutesPlayed.Value;

            MarkUpdatedNow();
        }
    }

}
