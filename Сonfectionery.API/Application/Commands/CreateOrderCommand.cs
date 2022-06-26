using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Сonfectionery.API.Application.DTOs;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.Domain.Aggregates.OrderAggregate;

namespace Сonfectionery.API.Application.Commands
{
    public class CreateOrderCommand : ICommand<bool>
    {
        public string Title { get; set; }

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
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Creating Order - Order: {@Title}", request.Title);

            var order = new Order(request.Title);

            foreach (var orderItem in request.OrderItems)
            {
                order.AddOrderItem(orderItem.PieId, orderItem.UnitPrice, orderItem.Discount, orderItem.Units);
            }

            _logger.LogInformation("----- Posting Order in the SQL DB - Order: {@Order}", order);

            await _orderRepository.AddAsync(order);
            await _orderRepository.UnitOfWork.CommitAsync(cancellationToken);

            return true;
        }
    }
}
