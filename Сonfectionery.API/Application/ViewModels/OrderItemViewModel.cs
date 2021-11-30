using System;

namespace Сonfectionery.API.Application.ViewModels
{
    public class OrderItemViewModel
    {
        public Guid OrderItemId { get; set; }
        public Guid PieId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Units { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public PieViewModel Pie { get; set; }
    }
}