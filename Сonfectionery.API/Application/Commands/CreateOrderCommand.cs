using Сonfectionery.API.Application.Interfaces;

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

        private bool ContainOrderItems(IEnumerable<OrderItemModel> orderItems)
        {
            return orderItems.Any();
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Order.Create(request.Title);

            foreach (var orderItem in request.OrderItems)
            {
                order.AddOrderItem(orderItem.PieId, orderItem.UnitPrice, orderItem.Discount, orderItem.Units);
            }

            _logger.LogInformation("----- Creating Order - Order: {@Order}", order);

            _orderRepository.AddAsync(order);
        }
    }
}
