using System.Data;

namespace Сonfectionery.Infrastructure.Processing.SqlConnection.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}