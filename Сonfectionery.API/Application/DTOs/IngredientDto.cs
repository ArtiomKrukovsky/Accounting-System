using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Сonfectionery.API.Application.DTOs
{
    public class IngredientDto
    {
        public string Name { get; set; }
        public bool IsAllergen { get; set; }
        public double RelativeAmount { get; set; }
    }
}
