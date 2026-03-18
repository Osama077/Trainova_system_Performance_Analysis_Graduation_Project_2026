using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.Profiles.TeamsStaff
{
    public class TeamStaff
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public TeamStaffRole Role { get; private set; }
    }
}
