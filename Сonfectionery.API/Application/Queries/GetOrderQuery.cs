using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.API.Application.ViewModels;
using Сonfectionery.Domain.Aggregates.OrderAggregate;

namespace Сonfectionery.API.Application.Queries
{
    [DataContract]
    public class GetOrderQuery : IQuery<OrderViewModel>
    {
        [DataMember]
        public Guid OrderId { get; set; }
    }

    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderViewModel>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderViewModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderId);

            return _mapper.Map<OrderViewModel>(order);
        }
    }
}
