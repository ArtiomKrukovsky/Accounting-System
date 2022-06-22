using Autofac;
using Сonfectionery.Infrastructure.Processing.EventsDispatcher;
using Сonfectionery.Infrastructure.Processing.EventsDispatcher.Interfaces;

namespace Сonfectionery.API.Application.Modules
{
    public class ProcessingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}