using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.MatchsManagement.Matches;
using Trainova.Domain.Profiles.Players;
using Trainova.Domain.SeasonsAnalyses.Teams;

namespace Trainova.Domain.MatchsManagement.Events
{
    public class Event : AuditableEntity<Guid>
    {
        public Guid MatchId { get; private set; }

        public Guid? PlayerId { get; private set; }

        public Guid? TeamId { get; private set; }

        public int Period { get; private set; }

        public TimeOnly Timestamp { get; private set; }

        public byte Minute { get; private set; }

        public byte Second { get; private set; }

        public string? EventType { get; private set; }

        public double? LocationX { get; private set; }

        public double? LocationY { get; private set; }

        public double? PassLength { get; private set; }

        public double? PassAngle { get; private set; }

        public string? PassOutcome { get; private set; }

        public double? PassEndX { get; private set; }

        public double? PassEndY { get; private set; }

        public bool? IsProgressivePass { get; private set; }

        public string? ShotOutcome { get; private set; }

        public double? ShotXg { get; private set; }

        public string? ShotTechnique { get; private set; }

        public double? ShotEndX { get; private set; }

        public double? ShotEndY { get; private set; }

        public bool? ShotAfterSetPiece { get; private set; }

        public double? PredictedXg { get; private set; }

        public string? BodyPart { get; private set; }

        public int? EventIndex { get; private set; }

        public double? CarryEndX { get; private set; }

        public double? CarryEndY { get; private set; }

        public string? DribbleOutcome { get; private set; }

        public double? PressureDuration { get; private set; }

        public bool? UnderPressure { get; private set; }

        public bool? CounterPress { get; private set; }


        private Event() : base()
        {
        }

        public Event(
            Guid matchId,
            Guid? playerId,
            Guid? teamId,
            int period,
            TimeOnly timestamp,
            byte minute,
            byte second,
            string? eventType,
            double? locationX = null,
            double? locationY = null,
            double? passLength = null,
            double? passAngle = null,
            string? passOutcome = null,
            double? passEndX = null,
            double? passEndY = null,
            bool? isProgressivePass = null,
            string? shotOutcome = null,
            double? shotXg = null,
            string? shotTechnique = null,
            double? shotEndX = null,
            double? shotEndY = null,
            bool? shotAfterSetPiece = null,
            double? predictedXg = null,
            string? bodyPart = null,
            int? eventIndex = null,
            double? carryEndX = null,
            double? carryEndY = null,
            string? dribbleOutcome = null,
            double? pressureDuration = null,
            bool? underPressure = null,
            bool? counterPress = null,
            Guid? createdBy = null) : base(createdBy)
        {

            MatchId = matchId;
            PlayerId = playerId;
            TeamId = teamId;
            Period = period;
            Timestamp = timestamp;
            Minute = minute;
            Second = second;
            EventType = eventType;

            LocationX = locationX;
            LocationY = locationY;
            PassLength = passLength;
            PassAngle = passAngle;
            PassOutcome = passOutcome;
            PassEndX = passEndX;
            PassEndY = passEndY;
            IsProgressivePass = isProgressivePass;
            ShotOutcome = shotOutcome;
            ShotXg = shotXg;
            ShotTechnique = shotTechnique;
            ShotEndX = shotEndX;
            ShotEndY = shotEndY;
            ShotAfterSetPiece = shotAfterSetPiece;
            PredictedXg = predictedXg;
            BodyPart = bodyPart;
            EventIndex = eventIndex;
            CarryEndX = carryEndX;
            CarryEndY = carryEndY;
            DribbleOutcome = dribbleOutcome;
            PressureDuration = pressureDuration;
            UnderPressure = underPressure;
            CounterPress = counterPress;
        }


        public void Update(
            double? locationX = null,
            double? locationY = null,
            double? passLength = null,
            double? passAngle = null,
            string? passOutcome = null,
            double? passEndX = null,
            double? passEndY = null,
            bool? isProgressivePass = null,
            string? shotOutcome = null,
            double? shotXg = null,
            string? shotTechnique = null,
            double? shotEndX = null,
            double? shotEndY = null,
            bool? shotAfterSetPiece = null,
            double? predictedXg = null,
            string? bodyPart = null,
            int? eventIndex = null,
            double? carryEndX = null,
            double? carryEndY = null,
            string? dribbleOutcome = null,
            double? pressureDuration = null,
            bool? underPressure = null,
            bool? counterPress = null)
        {
            MarkUpdatedNow();

            LocationX = locationX ?? LocationX;
            LocationY = locationY ?? LocationY;
            PassLength = passLength ?? PassLength;
            PassAngle = passAngle ?? PassAngle;
            PassOutcome = passOutcome ?? PassOutcome;
            PassEndX = passEndX ?? PassEndX;
            PassEndY = passEndY ?? PassEndY;
            IsProgressivePass = isProgressivePass ?? IsProgressivePass;
            ShotOutcome = shotOutcome ?? ShotOutcome;
            ShotXg = shotXg ?? ShotXg;
            ShotTechnique = shotTechnique ?? ShotTechnique;
            ShotEndX = shotEndX ?? ShotEndX;
            ShotEndY = shotEndY ?? ShotEndY;
            ShotAfterSetPiece = shotAfterSetPiece ?? ShotAfterSetPiece;
            PredictedXg = predictedXg ?? PredictedXg;
            BodyPart = bodyPart ?? BodyPart;
            EventIndex = eventIndex ?? EventIndex;
            CarryEndX = carryEndX ?? CarryEndX;
            CarryEndY = carryEndY ?? CarryEndY;
            DribbleOutcome = dribbleOutcome ?? DribbleOutcome;
            PressureDuration = pressureDuration ?? PressureDuration;
            UnderPressure = underPressure ?? UnderPressure;
            CounterPress = counterPress ?? CounterPress;

        }

    }

}
