using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.Infrastructure.EntityConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(o => o.Id).HasName("PK_OrderItem");

            builder.Property<Guid>("OrderId")
                .IsRequired();

            builder.Property<Guid>("PieId")
                .IsRequired();

            builder
                .Property<decimal>("_unitPrice")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("UnitPrice")
                .IsRequired();

            builder
                .Property<decimal>("_discount")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Discount")
                .IsRequired();

            builder
                .Property<int>("_units")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Units")
                .IsRequired();

            builder.HasOne<Pie>()
                .WithMany()
                .HasForeignKey(x => x.PieId);
        }
    }
}
