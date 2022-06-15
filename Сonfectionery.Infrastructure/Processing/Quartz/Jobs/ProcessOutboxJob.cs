using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Quartz;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Infrastructure.Processing.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        private readonly СonfectioneryContext _context;
        private readonly IMediator _mediator;

        public ProcessOutboxJob(
            СonfectioneryContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = _context.OutboxMessages.Where(x => x.ProcessedAt == null).ToList();

            foreach (var message in messages)
            {
                var type = Assembly.GetAssembly(typeof(IDomainEventNotification<>))?.GetType(message.Type);
                var notification = JsonConvert.DeserializeObject(message.Payload, type);

                await _mediator.Publish((INotification)notification);

                message.RefreshProcessedDate();

                await _context.CommitAsync();
            }
        }
    }
}