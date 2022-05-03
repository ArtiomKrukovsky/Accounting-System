using System;
using System.Collections.Generic;

namespace Сonfectionery.API.Application.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
    }
}
