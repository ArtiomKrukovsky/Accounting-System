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
            builder.ToTable("Orders", СonfectioneryContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .UseHiLo("orderseq", СonfectioneryContext.DEFAULT_SCHEMA);

            builder
                .Property<DateTime>("_title")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Title")
                .IsRequired();

            builder
                .Property<DateTime>("_orderDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderDate")
                .IsRequired();

            builder
                .Property<DateTime>("_orderStatusId")
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
