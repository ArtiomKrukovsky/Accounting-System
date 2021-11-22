using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task AddAsync(Order order)
        {
            _context.Orders.AddAsync(order);
            _context.Orders.SaveChangesAsync();
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return _context.Orders
                .Include(x => x.OrderItems)
                .ToListAsync();
        }

        public Task<Order> GetAsync(int pieId)
        {
            return _context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync();
        }
    }
}
