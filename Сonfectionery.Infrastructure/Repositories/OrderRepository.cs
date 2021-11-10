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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetAsync(int pieId)
        {
            throw new NotImplementedException();
        }
    }
}
