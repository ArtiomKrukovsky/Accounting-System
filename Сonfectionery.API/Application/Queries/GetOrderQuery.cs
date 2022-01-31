using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Сonfectionery.API.Application.Constants;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.API.Application.ViewModels;
using Сonfectionery.Services.Kafka;

namespace Сonfectionery.API.Application.Queries
{
    [DataContract]
    public class GetOrderQuery : IQuery<OrderViewModel>
    {
        [DataMember]
        public Guid OrderId { get; set; }

        public GetOrderQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }

    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderViewModel>
    {
        private readonly KSqlDbService<OrderViewModel> _kSqlDbService;

        public GetOrderQueryHandler(KSqlDbService<OrderViewModel> kSqlDbService)
        {
            _kSqlDbService = kSqlDbService;
        }

        public async Task<OrderViewModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            const string tableName = KafkaConstants.OrdersTable;

            var orderId = request.OrderId.ToString();
            var order = await _kSqlDbService.GetAsync(tableName, order => order.Id == orderId);

            return order;
        }
    }
}
