using Autofac;
using System.Reflection;
using Сonfectionery.Domain.Aggregates.OrderAggregate;
using Сonfectionery.Domain.Aggregates.PieAggregate;
using Сonfectionery.Domain.Seedwork;
using Сonfectionery.Infrastructure.Processing.SqlConnection;
using Сonfectionery.Infrastructure.Processing.SqlConnection.Interfaces;
using Сonfectionery.Infrastructure.Repositories;
using Module = Autofac.Module;

namespace Сonfectionery.API.Application.Modules
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectionString;

        public InfrastructureModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

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