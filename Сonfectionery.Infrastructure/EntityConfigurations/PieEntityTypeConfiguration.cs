using System;
using System.Collections.Generic;
using System.Text;

namespace Сonfectionery.Infrastructure.EntityConfigurations
{
    public class PieEntityTypeConfiguration : IEntityTypeConfiguration<Pie>
    {
        public void Configure(EntityTypeBuilder<Pie> builder)
        {
            builder
                .Property(x => x.Id)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Id")
                .Required();

            builder
                .Property(x => x.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .Required();

            builder
                .Property(x => x.Description)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Description")
                .Required();

            builder.OwnsOne(x => x.Portions);

            var ingridientsConfiguration = builder.OwnsMany(x => x.Ingredients);
            ingridientsConfiguration.SetPropertyAccessMode(PropertyAccessMode.Field);

            ingridientsConfiguration.Property(x => x.Name).HasMaxLength(250).IsRequired();
        }
    }
}
