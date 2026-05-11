using Trainova.Domain.UserAuth;

namespace Trainova.Application.Common.Interfaces.Repositories.UserAuth;

public interface IUsersRepository
{
    // Reading operations
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid userId);

    // Writing operations
    Task AddUserAsync(User user);
    Task UpdateAsync(User user);
    Task<IEnumerable<User>> GetByIdsAsync(List<Guid> guids);
}