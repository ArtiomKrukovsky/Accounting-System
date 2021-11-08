using System.Collections.Generic;
using System.Threading.Tasks;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.PieAggregate
{
    public interface IPieRepository : IRepository<Pie>
    {
        Task<IEnumerable<Pie>> GetAllAsync();

        Task<Pie> GetAsync(int pieId);

        Task AddAsync(Pie pie);
    }
}
