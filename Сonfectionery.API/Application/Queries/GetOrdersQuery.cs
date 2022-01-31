using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Сonfectionery.API.Application.Constants;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.API.Application.ViewModels;
using Сonfectionery.Services.Kafka;

namespace Сonfectionery.API.Application.Queries
{
    public class GetOrdersQuery : IQuery<IEnumerable<OrderViewModel>>
    { }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderViewModel>>
    {
        private readonly KSqlDbService<OrderViewModel> _kSqlDbService;

        public GetOrdersQueryHandler(KSqlDbService<OrderViewModel> kSqlDbService)
        {
            _kSqlDbService = kSqlDbService;
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            const string tableName = KafkaConstants.OrdersTable;

            var orders = await _kSqlDbService.GetAllAsync(tableName);

            return orders;
        }
    }
}
