using System;
using System.Threading;
using System.Threading.Tasks;

namespace Сonfectionery.Domain.Seedwork
{
    public interface IUnitOfWork: IDisposable
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<ITransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(ITransaction transaction);
        Task RollbackTransactionAsync();
    }
}