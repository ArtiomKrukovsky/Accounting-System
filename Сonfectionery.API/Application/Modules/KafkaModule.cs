using Autofac;
using Microsoft.Extensions.Options;
using Сonfectionery.Services.Configurations;
using Сonfectionery.Services.Kafka;

namespace Сonfectionery.API.Application.Modules
{
    public class KafkaModule : Module
    {
        private readonly KafkaConfig _kafkaConfig;

        public KafkaModule(KafkaConfig kafkaConfig)
        {
            _kafkaConfig = kafkaConfig;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(KafkaService<>))
                .As(typeof(IKafkaService<>))
                .SingleInstance();

            builder.Register(c => Options.Create(_kafkaConfig));
        }
    }
}