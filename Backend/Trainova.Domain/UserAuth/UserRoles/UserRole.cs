using Microsoft.EntityFrameworkCore;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.UserAuth.Roles;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.UserAuth.UserRoles
{
    public class UserRole : Entity<UserRoleId>
    {
        public Role Role { get; private set; }
        public User User { get; private set; }
        public UserRole(Guid userId, byte roleId) : base(new UserRoleId(roleId,userId)) { }
        private UserRole():base() { }
    }
    [Owned]
    public record UserRoleId(byte RoleId, Guid UserId);
}
