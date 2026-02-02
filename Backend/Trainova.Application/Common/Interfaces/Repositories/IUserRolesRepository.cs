using System.Data;
using Trainova.Domain.Users;

namespace Trainova.Application.Common.Interfaces.Repositories
{
    public interface IUserRolesRepository
    {
        Task AddAsync(UserRole userRole);
    }
}
