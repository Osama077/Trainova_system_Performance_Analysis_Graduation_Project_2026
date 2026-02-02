using Trainova.Domain.Users;

namespace Trainova.Domain.Coaches
{
    public class Coach
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public CoachRole Role { get; private set; }
    }

    public enum CoachRole
    {
        fitnessCoach,
        assistantCoach,
        headCoach
    }
}
