using Trainova.Domain.UserAuth.Users;

namespace Trainova.Application.Common.Interfaces.Repositories.UserAuth;

public interface IUsersRepository
{
    // Reading operations
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid userId);

    // Writing operations
    Task AddUserAsync(User user);
    void Update(User user);

}