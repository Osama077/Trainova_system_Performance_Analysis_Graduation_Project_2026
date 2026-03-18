using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Domain.UserAuth.Roles;

namespace Trainova.Infrastructure.DataAccess.Repositories.Users
{
    public class RolesRepository : IRolesRepository
    {
        public Task<Role?> GetRoleByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
