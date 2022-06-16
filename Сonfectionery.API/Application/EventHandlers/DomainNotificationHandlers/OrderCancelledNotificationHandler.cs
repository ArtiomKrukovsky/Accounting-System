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
    public class OrderCancelledNotificationHandler :  INotificationHandler<OrderCancelledNotification>
    {
        private readonly IKafkaService<Order> _kafkaService;
        private readonly ILogger<OrderCancelledNotificationHandler> _logger;

        public OrderCancelledNotificationHandler(
            IKafkaService<Order> kafkaService,
            ILogger<OrderCancelledNotificationHandler> logger)
        {
            _kafkaService = kafkaService ?? throw new ArgumentNullException(nameof(kafkaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderCancelledNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Sending Updated Order in Kafka via Cancelling - Order: {@Order}", notification.Order.Title);

            const string ordersTopic = KafkaConstants.OrdersTopic;
            const string orderKey = KafkaConstants.OrderKey;

            await _kafkaService.ProduceAsync(ordersTopic, orderKey, notification.Order);
        }
    }
}