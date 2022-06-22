using Autofac;
using System.Reflection;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Aggregates.PieAggregate;
using Сonfectionery.Domain.Seedwork;
using Сonfectionery.Infrastructure.Repositories;
using Module = Autofac.Module;

namespace Сonfectionery.API.Application.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderRepository>()
                .As<IOrderRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PieRepository>()
                .As<IPieRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IDomainEventNotification<>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IDomainEventNotification<>)).InstancePerDependency();
        }
    }
}