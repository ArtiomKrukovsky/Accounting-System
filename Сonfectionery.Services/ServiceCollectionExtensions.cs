using System;
using ksqlDb.RestApi.Client.DependencyInjection;
using ksqlDB.RestApi.Client.KSql.Query.Options;
using Microsoft.Extensions.DependencyInjection;
using Сonfectionery.Services.Kafka;
using Сonfectionery.Services.Kafka.Configurations;
using Сonfectionery.Services.Kafka.Producer;

namespace Сonfectionery.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaMessageBus(this IServiceCollection services)
            => services.AddSingleton(typeof(IKafkaMessageBus<,>), typeof(KafkaMessageBus<,>));

        public static IServiceCollection AddKafkaProducer(this IServiceCollection services,
            Action<KafkaProducerConfig> configAction)
        {
            services.AddSingleton(typeof(KafkaProducerService<,>));

            services.Configure(configAction);

            return services;
        }

        public static IServiceCollection AddKSqlDb(this IServiceCollection services,
            Action<KSqlDbConfig> configAction)
        {
            var kSqlDbConfig = new KSqlDbConfig();
            configAction.Invoke(kSqlDbConfig);

            services.ConfigureKSqlDb(kSqlDbConfig.BaseUrl, setupParameters =>
            {
                setupParameters.SetAutoOffsetReset(AutoOffsetReset.Earliest);
                setupParameters.Options.ShouldPluralizeFromItemName = kSqlDbConfig.ShouldPluralizeFromItemName;
            });

            return services;
        }
    }
}