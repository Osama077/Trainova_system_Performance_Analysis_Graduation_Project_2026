using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.UserAuth.UserRoles;

namespace Trainova.Domain.UserAuth.Roles
{
    public class Role
    {
        public byte Id { get; private set; }
        public string Name { get; private set; }
        public string NormalizedName { get; private set; }
        public ICollection<UserRole> Roles { get; private set; } = new List<UserRole>();

        public Role(byte id, string name)
        {
            Id = id;
            Name = name;
            NormalizedName = name.ToLowerInvariant();
        }

    }
}
