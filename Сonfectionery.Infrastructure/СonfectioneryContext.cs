using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Aggregates.PieAggregate;
using Сonfectionery.Domain.Seedwork;
using Сonfectionery.Infrastructure.EntityConfigurations;
using Сonfectionery.Infrastructure.Processing.EventsDispatcher.Interfaces;
using Сonfectionery.Infrastructure.Processing.Outbox;

namespace Сonfectionery.Infrastructure
{
    public class СonfectioneryContext: DbContext, IUnitOfWork
    {
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public СonfectioneryContext(DbContextOptions<СonfectioneryContext> options, 
            IDomainEventsDispatcher domainEventsDispatcher) : base(options)
        {
            _domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
        }

        public DbSet<Pie> Pies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PieEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _domainEventsDispatcher.DispatchEventsAsync();

            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
