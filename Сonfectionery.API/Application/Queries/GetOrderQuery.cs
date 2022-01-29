using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ksqlDB.RestApi.Client.KSql.Linq.PullQueries;
using ksqlDB.RestApi.Client.KSql.Query.Context;
using MediatR;
using Сonfectionery.API.Application.Constants;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.API.Application.ViewModels;

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
        private readonly IKSqlDBContext _kSqlDbContext;

        public GetOrderQueryHandler(IKSqlDBContext kSqlDbContext)
        {
            _kSqlDbContext = kSqlDbContext;
        }

        public async Task<OrderViewModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            const string tableName = KafkaConstants.OrdersTable;

            var orderId = request.OrderId.ToString();

            var order = await _kSqlDbContext.CreatePullQuery<OrderViewModel>(tableName)
                .Where(x => x.Id == orderId)
                .GetManyAsync(cancellationToken)
                .FirstOrDefaultAsync(cancellationToken);

            return order;
        }
    }
}
