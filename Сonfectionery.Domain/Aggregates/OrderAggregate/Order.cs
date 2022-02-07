using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void SetCancelledStatus()
        {
            if (_orderStatusId == OrderStatus.Paid.Id ||
                _orderStatusId == OrderStatus.Shipping.Id ||
                _orderStatusId == OrderStatus.Cooking.Id)
            {
                StatusChangeException(OrderStatus.Cancelled);
            }

            _orderStatusId = OrderStatus.Cancelled.Id;
        }

        public void RefreshStatus()
        {
            OrderStatus = OrderStatus.FromIdentifier(_orderStatusId);
        }

        public decimal GetSummaryPrice()
        {
            return OrderItems.Sum(orderItem => orderItem.TotalPrice);
        }

        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new ArgumentException($"Is not possible to change the order status from {OrderStatus.Name} to {orderStatusToChange.Name}.");
        }
    }
}
