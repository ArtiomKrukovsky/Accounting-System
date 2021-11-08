using System;
using System.Collections.Generic;
using System.Text;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.Infrastructure
{
    public class СonfectioneryContext
    {
        public СonfectioneryContext(DbContextOptions<СonfectioneryContext> options) : base(options) { }

        public DbSet<Pie> Pies { get; set; }

        protected override void СonfectioneryContext(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration()
        }
    }
}
