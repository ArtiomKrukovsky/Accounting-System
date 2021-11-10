using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.Infrastructure
{
    public class СonfectioneryContext
    {
        public const string DEFAULT_SCHEMA = "Сonfectionery";

        public СonfectioneryContext(DbContextOptions<СonfectioneryContext> options) : base(options) { }

        public DbSet<Pie> Pies { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void СonfectioneryContext(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration();
        }
    }
}
