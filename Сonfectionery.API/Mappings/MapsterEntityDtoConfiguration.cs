using Mapster;
using Сonfectionery.API.Application.DTOs;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.API.Mappings
{
    public class MapsterEntityDtoConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PortionsDto, Portions>()
                .Map(d => d.Maximum, s => s.Maximum)
                .Map(d => d.Minimum, s => s.Minimum);
            config.NewConfig<IngredientDto, Ingredient>()
                .Map(d => d.IsAllergen, s => s.IsAllergen)
                .Map(d => d.Name, s => s.Name)
                .Map(d => d.RelativeAmount, s => s.RelativeAmount);
        }
    }
}