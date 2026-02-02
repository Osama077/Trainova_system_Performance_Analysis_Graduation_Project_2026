using Trainova.Domain.Common;

namespace Trainova.Domain.Users
{
    public class UserRole : IAuditable
    {

        public byte RoleId { get; private set; }
        public Role Role { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? Owner { get; private set; }


    }
}
