using System;
using System.Collections.Generic;

namespace Сonfectionery.API.Application.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime OrderCreated { get; set; }
        public string OrderStatus { get; set; }
        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
    }
}
