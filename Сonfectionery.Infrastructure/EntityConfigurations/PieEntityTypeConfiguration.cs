using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.Infrastructure.EntityConfigurations
{
    public class PieEntityTypeConfiguration : IEntityTypeConfiguration<Pie>
    {
        public void Configure(EntityTypeBuilder<Pie> builder)
        {
            builder.ToTable("Pie");

            builder.HasKey(o => o.Id).HasName("PK_Pie");

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .IsRequired();

            builder.OwnsOne(x => x.Portions);

            var ingredientsConfiguration = builder.OwnsMany(x => x.Ingredients);
            builder.Navigation(x => x.Ingredients).Metadata.SetField("_ingredients");
            builder.Navigation(x => x.Ingredients).UsePropertyAccessMode(PropertyAccessMode.Field);

            ingredientsConfiguration.Property(x => x.Name).HasMaxLength(250).IsRequired();
        }
    }
}
