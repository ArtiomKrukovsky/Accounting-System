using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.API.Application.ViewModels;
using Сonfectionery.Domain.Aggregates.OrderAggregate;

namespace Сonfectionery.API.Application.Queries
{
    public class GetOrdersQuery : IQuery<IEnumerable<OrderViewModel>>
    { }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderViewModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<OrderViewModel>>(orders);
        }
    }
}
