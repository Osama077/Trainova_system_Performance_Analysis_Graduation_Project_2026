using Trainova.Application.Common.Models;
using Trainova.Domain.Outbox;

namespace Trainova.Application.Common.Interfaces.Repositories
{
    public interface IEmailOutboxRepository
    {
        Task AddAsync(EmailOutbox email);
        Task<IEnumerable<PendingEmail>> GetPendingAsync(int take = 50);
        void UpdateAsync(EmailOutbox email);

    }

}
