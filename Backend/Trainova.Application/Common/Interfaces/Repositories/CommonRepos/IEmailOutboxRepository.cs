using Trainova.Application.Common.Models;
using Trainova.Domain.Common.Outbox;

namespace Trainova.Application.Common.Interfaces.Repositories.CommonRepos
{
    public interface IEmailOutboxRepository
    {
        Task AddAsync(EmailOutbox email);
        Task<IEnumerable<PendingEmail>> GetPendingAsync(int take = 50);
        Task UpdateAsync(EmailOutbox email);

    }

}
