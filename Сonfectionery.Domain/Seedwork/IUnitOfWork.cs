using System;
using System.Threading;
using System.Threading.Tasks;

namespace Сonfectionery.Domain.Seedwork
{
    public interface IUnitOfWork: IDisposable
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}