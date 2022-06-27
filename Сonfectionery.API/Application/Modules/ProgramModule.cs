using System.Reflection;
using Autofac;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Module = Autofac.Module;

namespace Сonfectionery.API.Application.Modules
{
    public class ProgramModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Logging
            builder.RegisterInstance(new LoggerFactory())
                .As<ILoggerFactory>();

            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>))
                .SingleInstance();

            // Mapster
            var config = new TypeAdapterConfig { RequireExplicitMapping = true };
            config.Scan(Assembly.GetExecutingAssembly());
            config.Compile(); // validate mappings

            builder.RegisterInstance(config);

            builder.RegisterType<ServiceMapper>()
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}