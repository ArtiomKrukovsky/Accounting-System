using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Сonfectionery.Domain.Aggregates.OrderAggregate;

namespace Сonfectionery.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly СonfectioneryContext _context;

        public OrderRepository(СonfectioneryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .ToListAsync();
        }

        public async Task<Order> GetAsync(int pieId)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync();
        }
    }
}
