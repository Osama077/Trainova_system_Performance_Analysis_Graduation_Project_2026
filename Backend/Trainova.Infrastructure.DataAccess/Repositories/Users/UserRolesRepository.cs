using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Domain.Users;

namespace Trainova.Infrastructure.DataAccess.Repositories.Users
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;

        public UserRolesRepository(TrainovaWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UserRole userRole)
        {
            await _dbContext.UserRoles.AddAsync(userRole);
        }
    }
}
