using System.Data;

namespace Trainova.Application.Common.Interfaces.Services
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task StartTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        bool IsInTransaction { get; }
    }
}
