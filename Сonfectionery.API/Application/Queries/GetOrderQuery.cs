using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ksqlDB.RestApi.Client.KSql.Query.Context;
using MapsterMapper;
using MediatR;
using Newtonsoft.Json;
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
        private readonly IKSqlDBContext _context;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IKSqlDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderViewModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            const string tableName = "orders_view";

            var enumerable = _context.CreatePullQuery<object>(tableName)
                .GetManyAsync();

            var list = new List<OrderData>();

            await foreach (var item in enumerable.ConfigureAwait(false))
            {
                list.Add(JsonConvert.DeserializeObject<OrderData>(item.ToString()));
            }

            return _mapper.Map<OrderViewModel>(new OrderViewModel());
        }

        public class OrderData
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string CreateOrder { get; set; }
            public string Status { get; set; }
            public string OrderItemId { get; set; }
            public string PieId { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Units { get; set; }
            public decimal TotalPrice { get; set; }
            public decimal Discount { get; set; }
        }
    }
}
