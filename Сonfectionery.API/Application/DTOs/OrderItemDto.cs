using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Сonfectionery.API.Application.DTOs
{
    public class OrderItemDto
    {
        public Guid PieId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Units { get; set; }
    }
}
