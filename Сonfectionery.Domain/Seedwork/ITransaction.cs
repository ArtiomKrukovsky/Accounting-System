using System;
using System.Threading.Tasks;

namespace Сonfectionery.Domain.Seedwork
{
    public interface ITransaction : IDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}