using Trainova.Domain.Users;

namespace Trainova.Domain.Doctors
{
    public class Doctor
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }

    }
}
