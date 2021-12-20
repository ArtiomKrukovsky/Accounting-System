using Mapster;
using Сonfectionery.API.Application.ViewModels;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.API.Mappings
{
    public class MapsterEntityModelConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Portions, PortionsViewModel>()
                .Map(d => d.Minimum, s => s.Minimum)
                .Map(d => d.Maximum, s => s.Maximum);
            config.NewConfig<Ingredient, IngredientViewModel>()
                .Map(d => d.Name, s => s.Name)
                .Map(d => d.IsAllergen, s => s.IsAllergen)
                .Map(d => d.RelativeAmount, s => s.RelativeAmount);
            config.NewConfig<Pie, PieViewModel>()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.Name, s => s.Name)
                .Map(d => d.Description, s => s.Description)
                .Map(d => d.Portions, s => s.Portions)
                .Map(d => d.Ingredients, s => s.Ingredients);

            config.NewConfig<OrderItem, OrderItemViewModel>()
                .Map(d => d.OrderItemId, s => s.Id)
                .Map(d => d.PieId, s => s.PieId)
                .Map(d => d.UnitPrice, s => s.UnitPrice)
                .Map(d => d.Discount, s => s.Discount)
                .Map(d => d.Units, s => s.Units)
                .Map(d => d.TotalPrice, s => s.TotalPrice);
            config.NewConfig<Order, OrderViewModel>()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.Title, s => s.Title)
                .Map(d => d.OrderCreated, s => s.OrderDate)
                .Map(d => d.OrderStatus, s => s.OrderStatus.Name)
                .Map(d => d.OrderItems, s => s.OrderItems);
        }
    }
}