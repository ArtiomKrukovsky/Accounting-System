using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Сonfectionery.API.Application.Constants;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Events.DomainEventNotifications;
using Сonfectionery.Services.Kafka;

namespace Сonfectionery.API.Application.EventHandlers.DomainNotificationHandlers
{
    public class OrderCreatedNotificationHandler :  INotificationHandler<OrderCreatedNotification>
    {
        private readonly IKafkaService<Order> _kafkaService;
        private readonly ILogger<OrderCreatedNotificationHandler> _logger;

        public OrderCreatedNotificationHandler(
            IKafkaService<Order> kafkaService, 
            ILogger<OrderCreatedNotificationHandler> logger)
        {
            _kafkaService = kafkaService ?? throw new ArgumentNullException(nameof(kafkaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Sending Order in Kafka via Creating - Order: {@Order}", notification.Order);

            const string ordersTopic = KafkaConstants.OrdersTopic;
            const string orderKey = KafkaConstants.OrderKey;

            await _kafkaService.ProduceAsync(ordersTopic, orderKey, notification.Order);
        }
    }
}