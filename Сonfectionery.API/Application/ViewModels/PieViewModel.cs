using System;
using System.Collections.Generic;

namespace Сonfectionery.API.Application.ViewModels
{
    public class PieViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PortionsViewModel Portions { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }
    }
}
