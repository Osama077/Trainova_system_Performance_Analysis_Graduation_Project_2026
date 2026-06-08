namespace Trainova.Domain.Scouting.ValueObjects
{
    using Trainova.Domain.Common.Helpers;

    /// <summary>
    /// Value object representing player's skill assessment (0-100 scale)
    /// </summary>
    public class SkillAssessment
    {
        private const int MinSkillValue = 0;
        private const int MaxSkillValue = 100;

        public int Pace { get; private set; }
        public int Shooting { get; private set; }
        public int Dribbling { get; private set; }
        public int Passing { get; private set; }
        public int Physicality { get; private set; }
        public int Positioning { get; private set; }
        public int Defending { get; private set; }
        public int Vision { get; private set; }

        public SkillAssessment(
            int pace = 0,
            int shooting = 0,
            int dribbling = 0,
            int passing = 0,
            int physicality = 0,
            int positioning = 0,
            int defending = 0,
            int vision = 0)
        {
            ValidateSkill(pace, nameof(pace));
            ValidateSkill(shooting, nameof(shooting));
            ValidateSkill(dribbling, nameof(dribbling));
            ValidateSkill(passing, nameof(passing));
            ValidateSkill(physicality, nameof(physicality));
            ValidateSkill(positioning, nameof(positioning));
            ValidateSkill(defending, nameof(defending));
            ValidateSkill(vision, nameof(vision));

            Pace = pace;
            Shooting = shooting;
            Dribbling = dribbling;
            Passing = passing;
            Physicality = physicality;
            Positioning = positioning;
            Defending = defending;
            Vision = vision;
        }

        public void Update(
            int? pace = null,
            int? shooting = null,
            int? dribbling = null,
            int? passing = null,
            int? physicality = null,
            int? positioning = null,
            int? defending = null,
            int? vision = null)
        {
            if (pace.HasValue)
            {
                ValidateSkill(pace.Value, nameof(pace));
                Pace = pace.Value;
            }
            if (shooting.HasValue)
            {
                ValidateSkill(shooting.Value, nameof(shooting));
                Shooting = shooting.Value;
            }
            if (dribbling.HasValue)
            {
                ValidateSkill(dribbling.Value, nameof(dribbling));
                Dribbling = dribbling.Value;
            }
            if (passing.HasValue)
            {
                ValidateSkill(passing.Value, nameof(passing));
                Passing = passing.Value;
            }
            if (physicality.HasValue)
            {
                ValidateSkill(physicality.Value, nameof(physicality));
                Physicality = physicality.Value;
            }
            if (positioning.HasValue)
            {
                ValidateSkill(positioning.Value, nameof(positioning));
                Positioning = positioning.Value;
            }
            if (defending.HasValue)
            {
                ValidateSkill(defending.Value, nameof(defending));
                Defending = defending.Value;
            }
            if (vision.HasValue)
            {
                ValidateSkill(vision.Value, nameof(vision));
                Vision = vision.Value;
            }
        }

        private static void ValidateSkill(int value, string skillName)
        {
            if (value < MinSkillValue || value > MaxSkillValue)
                throw new DomainException(
                    $"Skill '{skillName}' must be between {MinSkillValue} and {MaxSkillValue}. Current value: {value}",
                    "DomainError_InvalidSkillValue");
        }
    }
}
