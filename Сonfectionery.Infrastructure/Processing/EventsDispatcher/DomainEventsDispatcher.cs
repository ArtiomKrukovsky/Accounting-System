using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using MediatR;
using Newtonsoft.Json;
using Сonfectionery.Domain.Seedwork;
using Сonfectionery.Infrastructure.Processing.EventsDispatcher.Interfaces;
using Сonfectionery.Infrastructure.Processing.Outbox;

namespace Сonfectionery.Infrastructure.Processing.EventsDispatcher
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILifetimeScope _scope;
        private readonly СonfectioneryContext _context;

        public DomainEventsDispatcher(IMediator mediator, ILifetimeScope scope, СonfectioneryContext context)
        {
            _mediator = mediator;
            _scope = scope;
            _context = context;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (var domainEvent in domainEvents)
            {
                var domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);

            foreach (var domainEventNotification in domainEventNotifications)
            {
                var type = domainEventNotification.GetType().FullName;
                var data = JsonConvert.SerializeObject(domainEventNotification);

                var outboxMessage = OutboxMessage.Create(
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);

                _context.OutboxMessages.Add(outboxMessage);
            }
        }
    }
}