using System;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Dapper;
using Quartz;
using Сonfectionery.Domain.Seedwork;
using Сonfectionery.Infrastructure.Processing.Outbox;
using Сonfectionery.Infrastructure.Processing.SqlConnection.Interfaces;

namespace Сonfectionery.Infrastructure.Processing.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMediator _mediator;

        public ProcessOutboxJob(
            ISqlConnectionFactory sqlConnectionFactory,
            IMediator mediator)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                               "[OutboxMessage].[Id], " +
                               "[OutboxMessage].[Type], " +
                               "[OutboxMessage].[Payload] " +
                               "FROM [OutboxMessage]" +
                               "WHERE [OutboxMessage].[ProcessedAt] IS NULL";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);

            foreach (var message in messages)
            {
                var type = Assembly.GetAssembly(typeof(IDomainEventNotification<>))?.GetType(message.Type);
                var notification = JsonConvert.DeserializeObject(message.Payload, type);

                await _mediator.Publish((INotification)notification);

                const string sqlInsert = "UPDATE [OutboxMessage] " +
                                         "SET [ProcessedAt] = @Date " +
                                         "WHERE [Id] = @Id";

                await connection.ExecuteAsync(sqlInsert, new
                {
                    Date = DateTime.UtcNow,
                    message.Id
                });
            }
        }
    }
}