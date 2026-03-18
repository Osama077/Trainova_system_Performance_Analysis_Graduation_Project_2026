using System.Data;
using Trainova.Domain.UserAuth.UserRoles;

namespace Trainova.Application.Common.Interfaces.Repositories.UserAuth
{
    public interface IUserRolesRepository
    {
        Task AddAsync(UserRole userRole);
    }
}
