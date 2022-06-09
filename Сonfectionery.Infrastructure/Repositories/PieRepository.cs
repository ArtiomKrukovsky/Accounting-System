using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Сonfectionery.Domain.Aggregates.PieAggregate;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Infrastructure.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly СonfectioneryContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PieRepository(СonfectioneryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Pie>> GetAllAsync()
        {
            return await _context.Pies
                .Include(x => x.Portions).AsSingleQuery()
                .Include(x => x.Ingredients).AsSingleQuery()
                .ToListAsync();
        }

        public async Task<Pie> GetAsync(Guid pieId)
        {
            return await _context.Pies
                .Include(x => x.Portions).AsSingleQuery()
                .Include(x => x.Ingredients).AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == pieId);
        }

        public async Task<Pie> AddAsync(Pie pie)
        {
            await _context.Pies.AddAsync(pie);
            await _context.SaveChangesAsync();

            return pie;
        }
    }
}
