using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.Infrastructure.EntityConfigurations
{
    public class PieEntityTypeConfiguration : IEntityTypeConfiguration<Pie>
    {
        public void Configure(EntityTypeBuilder<Pie> builder)
        {
            builder.ToTable("Pies", СonfectioneryContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .UseHiLo("orderseq", СonfectioneryContext.DEFAULT_SCHEMA);

            builder
                .Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .IsRequired();

            builder
                .Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Description")
                .IsRequired();

            builder.OwnsOne(x => x.Portions);

            var ingridientsConfiguration = builder.OwnsMany(x => x.Ingredients);
            builder.Navigation(x => x.Ingredients).Metadata.SetField("_ingredients");
            builder.Navigation(x => x.Ingredients).UsePropertyAccessMode(PropertyAccessMode.Field);

            ingridientsConfiguration.Property(x => x.Name).HasMaxLength(250).IsRequired();
        }
    }
}
