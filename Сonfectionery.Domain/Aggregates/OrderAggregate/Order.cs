using System;
using System.Collections.Generic;
using System.Linq;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public string Title { get; private set; }
        public DateTime OrderDate { get; private set; }

        private int _orderStatusId;
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        private Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public static Order Create(string title)
        {
            return new Order
            {
                Title = title,
                _orderStatusId = OrderStatus.Submitted.Id,
                OrderDate = DateTime.UtcNow
            };
        }

        public void AddOrderItem(Guid pieId, decimal unitPrice, decimal discount, int units)
        {
            var existingOrderForPie = _orderItems.FirstOrDefault(x => x.PieId == pieId);

            if (existingOrderForPie != null)
            {
                existingOrderForPie.AddUnits(units);
            }
            else
            {
                var orderItem = OrderItem.Create(pieId, unitPrice, discount, units);
                _orderItems.Add(orderItem);
            }
        }
    }
}
