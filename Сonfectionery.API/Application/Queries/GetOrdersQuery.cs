using System.Collections;
using System.Collections.Generic;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.API.Application.ViewModels;

namespace Сonfectionery.API.Application.Queries
{
    public class GetOrdersQuery : IQuery<IEnumerable<OrderViewModel>>
    {
    }
}
