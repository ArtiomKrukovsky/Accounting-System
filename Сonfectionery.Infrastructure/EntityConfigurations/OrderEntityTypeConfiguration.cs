using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Сonfectionery.Domain.Aggregates.OrderAggregate;

namespace Сonfectionery.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.Id).HasName("PK_Order");

            builder
                .Property<string>("_title")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Title")
                .IsRequired();

            builder
                .Property<DateTime>("_orderDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderDate")
                .IsRequired();

            builder
                .Property<int>("_orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderStatusId")
                .IsRequired();

            builder.Navigation(x => x.OrderItems).Metadata.SetField("_orderItems");
            builder.Navigation(x => x.OrderItems).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(o => o.OrderStatus)
                .WithMany()
                .HasForeignKey("_orderStatusId");
        }
    }
}
