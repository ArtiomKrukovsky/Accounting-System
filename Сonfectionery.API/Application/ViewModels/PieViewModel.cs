using System.Collections.Generic;

namespace Сonfectionery.API.Application.ViewModels
{
    public class PieViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinimumPortions { get; set; }
        public int MaximumPortions { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }
    }
}
