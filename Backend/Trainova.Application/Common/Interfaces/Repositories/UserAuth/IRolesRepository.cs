using Trainova.Domain.UserAuth.Roles;

namespace Trainova.Application.Common.Interfaces.Repositories.UserAuth
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId);
        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}
