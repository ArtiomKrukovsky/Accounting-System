using Newtonsoft.Json;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Events.DomainEvents;

namespace Сonfectionery.Domain.Events.DomainEventNotifications
{
    public class OrderCancelledNotification : DomainNotificationBase<OrderCancelledEvent>
    {
        public Order Order { get; set; }

        public OrderCancelledNotification(OrderCancelledEvent domainEvent) : base(domainEvent)
        {
            Order = domainEvent.Order;
        }

        /// <summary>
        /// Deserialize message of event from JSON string
        /// </summary>
        /// <param name="order"></param>
        [JsonConstructor]
        public OrderCancelledNotification(Order order) : base(null)
        {
            Order = order;
        }
    }
}