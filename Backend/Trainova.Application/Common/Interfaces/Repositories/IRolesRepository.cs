using Trainova.Domain.Users;

namespace Trainova.Application.Common.Interfaces.Repositories
{
    public interface IRolesRepository
    {
        Task<byte> GetRoleIdByNameAsync(string roleName);
        Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId);
        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}
