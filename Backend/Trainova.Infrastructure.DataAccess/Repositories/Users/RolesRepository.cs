using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Domain.Users;

namespace Trainova.Infrastructure.DataAccess.Repositories.Users
{
    public class RolesRepository : IRolesRepository
    {
        public Task<Role?> GetRoleByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<byte> GetRoleIdByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
