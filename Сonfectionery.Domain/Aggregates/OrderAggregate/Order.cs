using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Сonfectionery.Domain.Events.DomainEvents;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.OrderAggregate
{
    [JsonObject]
    public class Order : Entity, IAggregateRoot
    {
        [JsonProperty]
        public string Title { get; private set; }
        [JsonProperty]
        public DateTime OrderDate { get; private set; }

        [JsonProperty]
        private int _orderStatusId;
        public OrderStatus OrderStatus { get; private set; }

        [JsonProperty]
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        private Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Order(string title) : this()
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            Title = title;
            _orderStatusId = OrderStatus.Submitted.Id;
            OrderDate = DateTime.UtcNow;

            AddDomainEvent(new OrderCreatedEvent(this));
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

            AddDomainEvent(new OrderCancelledEvent(this));
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
