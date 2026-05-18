using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Domain.Common.Outbox;

namespace Trainova.Infrastructure.DataAccess.Repositories.Common
{
    public class EventOutboxRepository : IEventOutboxRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;

        public EventOutboxRepository(TrainovaWriteDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(DomainEventOutbox domainEvent)
        {
            if (domainEvent == null) throw new ArgumentNullException(nameof(domainEvent));

            await _dbContext.DomainEventOutboxes.AddAsync(domainEvent);
        }

        public async Task AddRangeAsync(IEnumerable<DomainEventOutbox> domainEvents)
        {
            if (domainEvents == null) throw new ArgumentNullException(nameof(domainEvents));

            await _dbContext.DomainEventOutboxes.AddRangeAsync(domainEvents);
        }

        public async Task<IEnumerable<DomainEventOutbox>> GetAllAsync(int? take = null, bool OnlyUnhandled = true)
        {
            var query = _dbContext.DomainEventOutboxes.AsQueryable();

            if (OnlyUnhandled)
            {
                query = query.Where(e => !e.IsHandled);
            }

            query = query.OrderBy(e => e.CreatedAt);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
