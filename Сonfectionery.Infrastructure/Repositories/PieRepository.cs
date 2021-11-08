using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.Infrastructure.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly СonfectioneryContext _context;

        public PieRepository(СonfectioneryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Pie>> GetAllAsync()
        {
            return await _context.Pies
                .Include(x => x.Portions).AsSingleQuery()
                .Include(x => x.Ingredients).AsSingleQuery()
                .ToList();
        }

        public async Task<Pie> GetAsync(int pieId)
        {
            return await _context.Pies
                .Include(x => x.Portions).AsSingleQuery()
                .Include(x => x.Ingredients).AsSingleQuery()
                .FirstOrDefault(x => x.Id == pieId);
        }

        public async Task AddAsync(Pie pie)
        {
            await _context.Pies.AddAsync(pie);
            await _context.SaveChangesAsync();
        }
    }
}
