using System;
using System.Data;
using Сonfectionery.Infrastructure.Processing.SqlConnection.Interfaces;

namespace Сonfectionery.Infrastructure.Processing.SqlConnection
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection is not { State: ConnectionState.Open })
            {
                _connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }

        public void Dispose()
        {
            if (_connection is { State: ConnectionState.Open })
            {
                _connection.Dispose();
            }
        }
    }
}