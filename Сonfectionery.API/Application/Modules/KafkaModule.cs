using System;
using Autofac;
using Microsoft.Extensions.Options;
using Сonfectionery.Services.Configurations;
using Сonfectionery.Services.Kafka;
using Сonfectionery.Services.KSqlDb;

namespace Сonfectionery.API.Application.Modules
{
    public class KafkaModule : Module
    {
        private readonly KafkaConfig _kafkaConfig;
        private readonly KSqlDbConfig _kSqlDbConfig;

        public KafkaModule(KafkaConfig kafkaConfig, KSqlDbConfig kSqlDbConfig)
        {
            _kafkaConfig = kafkaConfig;
            _kSqlDbConfig = kSqlDbConfig;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(KafkaService<>))
                .As(typeof(IKafkaService<>))
                .SingleInstance();

            builder.RegisterGeneric(typeof(KSqlDbService<>))
                .As(typeof(IKSqlDbService<>))
                .SingleInstance();

            builder.Register(c => Options.Create(_kafkaConfig));
            builder.Register(c => Options.Create(_kSqlDbConfig));
        }
    }
}