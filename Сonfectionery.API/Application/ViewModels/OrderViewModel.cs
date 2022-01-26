using System;
using System.Collections.Generic;

namespace Сonfectionery.API.Application.ViewModels
{
    public class OrderViewModel
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

        //public Guid Id { get; set; }
        //public string Title { get; set; }
        //public DateTime OrderCreated { get; set; }
        //public string OrderStatus { get; set; }
        //public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
    }
}
