using System.Text.Json.Serialization;
using Сonfectionery.Domain.Aggregates.PieAggregate;
using Сonfectionery.Domain.Events.DomainEvents;

namespace Сonfectionery.Domain.Events.DomainEventNotifications
{
    public class PieCreatedNotification : DomainNotificationBase<PieCreatedEvent>
    {
        public Pie Pie { get; set; }

        public PieCreatedNotification(PieCreatedEvent domainEvent) : base(domainEvent)
        {
            Pie = domainEvent.Pie;
        }

        /// <summary>
        /// Deserialize message of event from JSON string
        /// </summary>
        /// <param name="pie"></param>
        [JsonConstructor]
        public PieCreatedNotification(Pie pie) : base(null)
        {
            Pie = pie;
        }
    }
}