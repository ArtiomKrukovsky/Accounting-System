using Сonfectionery.Domain.Aggregates.OrderAggregate;

namespace Сonfectionery.Domain.Events.DomainEvents
{
    public class OrderCancelledEvent : DomainEventBase
    {
        public Order Order { get; }

        public OrderCancelledEvent(Order order)
        {
            Order = order;
        }
    }
}