using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Сonfectionery.API.Application.Constants;
using Сonfectionery.Domain.Aggregates.PieAggregate;
using Сonfectionery.Domain.Events.DomainEventNotifications;
using Сonfectionery.Services.Kafka;

namespace Сonfectionery.API.Application.EventHandlers.DomainNotificationHandlers
{
    public class PieCreatedNotificationHandler :  INotificationHandler<PieCreatedNotification>
    {
        private readonly IKafkaService<Pie> _kafkaService;
        private readonly ILogger<PieCreatedNotificationHandler> _logger;

        public PieCreatedNotificationHandler(
            IKafkaService<Pie> kafkaService,
            ILogger<PieCreatedNotificationHandler> logger)
        {
            _kafkaService = kafkaService ?? throw new ArgumentNullException(nameof(kafkaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PieCreatedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Sending Pie in Kafka via Creating - Pie: {@Pie}", notification.Pie);

            const string piesTopic = KafkaConstants.PiesTopic;
            const string pieKey = KafkaConstants.PieKey;

            var pie = notification.Pie;

            pie.ClearDomainEvents();

            await _kafkaService.ProduceAsync(piesTopic, pieKey, pie);
        }
    }
}