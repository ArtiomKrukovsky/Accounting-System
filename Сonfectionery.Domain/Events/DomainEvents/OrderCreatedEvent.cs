using Сonfectionery.Domain.Aggregates.OrderAggregate;

namespace Сonfectionery.Domain.Events.DomainEvents
{
    public class OrderCreatedEvent : DomainEventBase
    {
        public Order Order { get; }

        public OrderCreatedEvent(Order order)
        {
            Order = order;
        }
    }
}