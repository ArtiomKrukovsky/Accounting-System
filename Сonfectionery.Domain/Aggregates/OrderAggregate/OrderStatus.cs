using System.Collections.Generic;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submitted = new OrderStatus(1, nameof(Submitted).ToLowerInvariant());
        public static OrderStatus Paid = new OrderStatus(2, nameof(Paid).ToLowerInvariant());
        public static OrderStatus Cooking = new OrderStatus(3, nameof(Cooking).ToLowerInvariant());
        public static OrderStatus Shipping = new OrderStatus(4, nameof(Shipping).ToLowerInvariant());
        public static OrderStatus Cancelled = new OrderStatus(5, nameof(Cancelled).ToLowerInvariant());

        public OrderStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<OrderStatus> List() => new List<OrderStatus> { Submitted, Paid, Cooking, Shipping, Cancelled };
    }
}
