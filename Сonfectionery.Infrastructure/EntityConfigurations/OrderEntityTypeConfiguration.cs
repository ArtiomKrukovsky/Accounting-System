using System;
using System.Collections.Generic;
using System.Text;

namespace Сonfectionery.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", СonfectioneryContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
        }
    }
}
