using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Сonfectionery.Domain.Events.DomainEventNotifications;

namespace Сonfectionery.API.Application.EventHandlers.DomainNotificationHandlers
{
    public class OrderCreatedNotificationHandler :  INotificationHandler<OrderCreatedNotification>
    {
        public Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
        {
            //TODO: send event to kafka

            return Task.CompletedTask;
        }
    }
}