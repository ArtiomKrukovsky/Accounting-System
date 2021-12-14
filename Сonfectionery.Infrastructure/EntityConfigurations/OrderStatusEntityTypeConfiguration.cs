using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Сonfectionery.Domain.Aggregates.OrderAggregate;

namespace Сonfectionery.Infrastructure.EntityConfigurations
{
    public class OrderStatusEntityTypeConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStatus");

            builder.HasKey(o => o.Id).HasName("PK_OrderStatus");

            builder.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}