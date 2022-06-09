using System;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Events
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            OccurredOn = DateTime.UtcNow;
        }

        public DateTime OccurredOn { get; }
    }
}