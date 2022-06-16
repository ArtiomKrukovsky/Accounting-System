using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.Domain.Events.DomainEvents
{
    public class PieCreatedEvent : DomainEventBase
    {
        public Pie Pie { get; }

        public PieCreatedEvent(Pie pie)
        {
            Pie = pie;
        }
    }
}