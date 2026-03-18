using Trainova.Domain.Profiles.Players;

namespace Trainova.Application.Common.Interfaces.Repositories.Profiles.Players
{
    public interface IPlayerRepository
    {
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);

    }
}
