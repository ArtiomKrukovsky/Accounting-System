﻿using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Сonfectionery.API.Application.Constants;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Services.Kafka;

namespace Сonfectionery.API.Application.Commands
{
    [DataContract]
    public class CancelOrderCommand : ICommand<bool>
    {
        [DataMember]
        public Guid Id { get; set; }

        public CancelOrderCommand(Guid id)
        {
            Id = id;
        }
    }

    public class CancelOrderCommandValidation : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("No orderId found");
        }
    }

    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IKafkaService<Order> _kafkaService;
        private readonly ILogger<CancelOrderCommandHandler> _logger;

        public CancelOrderCommandHandler(
            IOrderRepository orderRepository, 
            IKafkaService<Order> kafkaService, 
            ILogger<CancelOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _kafkaService = kafkaService ?? throw new ArgumentNullException(nameof(kafkaService)); 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        }

        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository.GetAsync(request.Id);

            if (existingOrder == null)
            {
                throw new ArgumentNullException(nameof(existingOrder));
            }

            _logger.LogInformation("----- Cancelling Order - Order: {@Order}", existingOrder.Title);

            existingOrder.SetCancelledStatus();

            _logger.LogInformation("----- Updating Order in the SQL DB - Order: {@Order}", existingOrder.Title);

            _orderRepository.Update(existingOrder);
            await _orderRepository.UnitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("----- Sending Updated Order in Kafka - Order: {@Order}", existingOrder.Title);

            existingOrder.RefreshStatus();

            const string ordersTopic = KafkaConstants.OrdersTopic;
            const string orderKey = KafkaConstants.OrderKey;

            await _kafkaService.ProduceAsync(ordersTopic, orderKey, existingOrder);

            return true;
        }
    }
}