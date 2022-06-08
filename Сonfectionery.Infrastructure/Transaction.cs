using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Infrastructure
{
    public class Transaction : ITransaction
    {
        private readonly IDbContextTransaction _transaction;

        public Transaction(IDbContextTransaction transaction)
        {
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}