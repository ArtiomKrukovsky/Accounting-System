using Autofac;
using Microsoft.Extensions.Options;
using Сonfectionery.Services.Configurations;
using Сonfectionery.Services.KSqlDb;

namespace Сonfectionery.API.Application.Modules
{
    public class KSqlDbModule : Module
    {
        private readonly KSqlDbConfig _kSqlDbConfig;

        public KSqlDbModule(KSqlDbConfig kSqlDbConfig)
        {
            _kSqlDbConfig = kSqlDbConfig;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(KSqlDbService<>))
                .As(typeof(IKSqlDbService<>))
                .SingleInstance();

            builder.Register(c => Options.Create(_kSqlDbConfig));
        }
    }
}