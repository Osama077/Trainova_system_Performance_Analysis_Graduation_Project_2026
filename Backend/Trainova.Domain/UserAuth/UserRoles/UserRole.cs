using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.UserAuth.Roles;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.UserAuth.UserRoles
{
    public class UserRole :ICreatorLogable
    {
        public byte RoleId { get; private set; }
        public Guid UserId { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public Guid? CreatedBy { get; private set; }
        public Role Role { get; private set; }
        public User User { get; private set; }
        private UserRole() { }

        public UserRole(byte roleId, Guid userId,Guid createdBy = default)
        {
            RoleId = roleId;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void SetCreator(Guid creatorId)
        {
            if (CreatedBy is not null)
                return;

            CreatedBy = creatorId;
        }
    }

}
