using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Сonfectionery.Domain.Events.DomainEventNotifications;

namespace Сonfectionery.API.Application.EventHandlers.DomainNotificationHandlers
{
    public class OrderCancelledNotificationHandler :  INotificationHandler<OrderCancelledNotification>
    {
        public Task Handle(OrderCancelledNotification notification, CancellationToken cancellationToken)
        {
            //TODO: send event to kafka

            return Task.CompletedTask;
        }
    }
}