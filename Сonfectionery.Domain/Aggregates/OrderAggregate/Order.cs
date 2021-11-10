using System;
using System.Collections.Generic;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        private DateTime _orderDate;

        private int _orderStatusId;
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        private Order()
        {

        }

        public static Order Create()
        {
            return new Order
            {
                _orderStatusId = OrderStatus.Submitted.Id,
                _orderDate = DateTime.UtcNow
            };
        }

        public void AddOrderItem()
        {

        }
    }
}
