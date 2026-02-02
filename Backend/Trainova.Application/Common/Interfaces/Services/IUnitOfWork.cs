using System.Data;

namespace Trainova.Application.Common.Interfaces.Services
{
    public interface IUnitOfWork
    {
        Task<int> CommitChangesAsync();
        Task StartTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
