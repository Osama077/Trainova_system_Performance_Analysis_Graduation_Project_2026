using Microsoft.EntityFrameworkCore;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.UserAuth.Roles;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.UserAuth.UserRoles
{
    public class UserRole : IAuditable<Guid>
    {

        public Guid Id { get; private set; }
        public byte RoleId { get; private set; }
        public Role Role { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        object IAuditable.Id => Id;

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? CreatedBy { get; private set; }


    }

}
