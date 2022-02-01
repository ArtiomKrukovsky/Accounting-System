using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Сonfectionery.Services.KSqlDb
{
    public interface IKSqlDbService<TValue>
    {
        Task<TValue> GetAsync(string tableName, Expression<Func<TValue, bool>> predicate = null);
        Task<List<TValue>> GetAllAsync(string tableName);
    }
}