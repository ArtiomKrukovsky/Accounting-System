using System.Text.Json.Serialization;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Events.DomainEvents;

namespace Сonfectionery.Domain.Events.DomainEventNotifications
{
    public class OrderCreatedNotification : DomainNotificationBase<OrderCreatedEvent>
    {
        public Order Order { get; set; }

        public OrderCreatedNotification(OrderCreatedEvent domainEvent) : base(domainEvent)
        {
            Order = domainEvent.Order;
        }

        /// <summary>
        /// Deserialize message of event from JSON string
        /// </summary>
        /// <param name="order"></param>
        [JsonConstructor]
        public OrderCreatedNotification(Order order) : base(null)
        {
            Order = order;
        }
    }
}