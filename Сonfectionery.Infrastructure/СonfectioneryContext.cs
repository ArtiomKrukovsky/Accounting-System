using Microsoft.EntityFrameworkCore;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Aggregates.PieAggregate;
using Сonfectionery.Infrastructure.EntityConfigurations;

namespace Сonfectionery.Infrastructure
{
    public class СonfectioneryContext: DbContext
    {
        public СonfectioneryContext(DbContextOptions<СonfectioneryContext> options) : base(options) { }

        public DbSet<Pie> Pies { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PieEntityTypeConfiguration());
        }
    }
}
