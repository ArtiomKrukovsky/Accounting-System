using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Сonfectionery.API.Application.DTOs;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Services.Kafka;

namespace Сonfectionery.API.Application.Commands
{
    [DataContract]
    public class CreateOrderCommand : ICommand<bool>
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public IEnumerable<OrderItemDto> OrderItems { get; set; }

        public CreateOrderCommand(string title, IEnumerable<OrderItemDto> orderItems)
        {
            Title = title;
            OrderItems = orderItems;
        }
    }

    public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation()
        {
            RuleFor(command => command.Title).NotEmpty();
            RuleFor(command => command.OrderItems).Must(ContainOrderItems).WithMessage("No order items found");
        }

        private static bool ContainOrderItems(IEnumerable<OrderItemDto> orderItems)
        {
            return orderItems.Any();
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IKafkaService<string, Order> _kafkaService;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            IKafkaService<string, Order> kafkaService,
            ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _kafkaService = kafkaService ?? throw new ArgumentNullException(nameof(kafkaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Creating Order - Order: {@Title}", request.Title);

            var order = Order.Create(request.Title);

            foreach (var orderItem in request.OrderItems)
            {
                order.AddOrderItem(orderItem.PieId, orderItem.UnitPrice, orderItem.Discount, orderItem.Units);
            }

            _logger.LogInformation("----- Posting Order in the SQL DB - Order: {@Order}", order);

            await _orderRepository.AddAsync(order);

            _logger.LogInformation("----- Sending Order in Kafka - Order: {@Order}", order);

            order.RefreshStatus();

            await _kafkaService.ProduceAsync("order", order);

            return true;
        }
    }
}
