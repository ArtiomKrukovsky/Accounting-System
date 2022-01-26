using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ksqlDB.RestApi.Client.KSql.Query.Context;
using MediatR;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.API.Application.ViewModels;

namespace Сonfectionery.API.Application.Queries
{
    public class GetOrdersQuery : IQuery<IEnumerable<OrderViewModel>>
    { }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderViewModel>>
    {
        private readonly IKSqlDBContext _kSqlDbContext;

        public GetOrdersQueryHandler(IKSqlDBContext kSqlDbContext)
        {
            _kSqlDbContext = kSqlDbContext;
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            const string tableName = "orders_view";

            var orders = await _kSqlDbContext.CreatePullQuery<OrderViewModel>(tableName)
                .GetManyAsync(cancellationToken)
                .ToListAsync(cancellationToken);

            return orders;
        }
    }
}
