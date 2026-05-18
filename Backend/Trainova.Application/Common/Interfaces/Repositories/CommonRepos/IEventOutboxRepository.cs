using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Outbox;

namespace Trainova.Application.Common.Interfaces.Repositories.CommonRepos
{
    public interface IEventOutboxRepository
    {
        Task<IEnumerable<DomainEventOutbox>> GetAllAsync(int? take = null,bool OnlyUnhandled = true);
        Task AddAsync(DomainEventOutbox domainEvent);
        Task AddRangeAsync(IEnumerable<DomainEventOutbox> domainEvents);

    }
}
