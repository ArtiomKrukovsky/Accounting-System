using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Aggregates.PieAggregate;
using Сonfectionery.Domain.Seedwork;
using Сonfectionery.Infrastructure.EntityConfigurations;

namespace Сonfectionery.Infrastructure
{
    public class СonfectioneryContext: DbContext, IUnitOfWork
    {
        public СonfectioneryContext(DbContextOptions<СonfectioneryContext> options) : base(options) { }

        public DbSet<Pie> Pies { get; set; }
        public DbSet<Order> Orders { get; set; }

        private ITransaction _currentTransaction;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PieEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //todo: dispatch domain events

            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ITransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            var dbTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            _currentTransaction = new Transaction(dbTransaction);
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(ITransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
