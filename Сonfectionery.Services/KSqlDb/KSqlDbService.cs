using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ksqlDB.RestApi.Client.KSql.Linq.PullQueries;
using ksqlDB.RestApi.Client.KSql.Query.Context;
using ksqlDB.RestApi.Client.KSql.Query.Options;
using Microsoft.Extensions.Options;
using Сonfectionery.Services.Configurations;

namespace Сonfectionery.Services.KSqlDb
{
    public class KSqlDbService<TValue> : IKSqlDbService<TValue> where TValue: class
    {
        private readonly IKSqlDBContext _kSqlDbContext;

        public KSqlDbService(IOptions<KSqlDbConfig> config)
        {
            var contextOptions = new KSqlDBContextOptions(config.Value.BaseUrl)
            {
                QueryStreamParameters = { AutoOffsetReset = AutoOffsetReset.Earliest },
                ShouldPluralizeFromItemName = config.Value.ShouldPluralizeFromItemName
            };

            _kSqlDbContext = new KSqlDBContext(contextOptions);
        }

        public async Task<TValue> GetAsync(string tableName, Expression<Func<TValue, bool>> predicate = null)
        {
            return await _kSqlDbContext.CreatePullQuery<TValue>(tableName)
                .Where(predicate)
                .GetManyAsync()
                .FirstOrDefaultAsync();
        }

        public async Task<List<TValue>> GetAllAsync(string tableName)
        {
            return await _kSqlDbContext.CreatePullQuery<TValue>(tableName)
                .GetManyAsync()
                .ToListAsync();
        }

        public void Dispose()
        {
            _kSqlDbContext?.DisposeAsync();
        }
    }
}