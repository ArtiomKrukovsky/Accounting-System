using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetAsync(Guid orderId);

        Task<Order> AddAsync(Order order);

        Task<Order> UpdateAsync(Order order);
    }
}
