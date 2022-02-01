using System;
using Microsoft.Extensions.DependencyInjection;
using Сonfectionery.Services.Configurations;
using Сonfectionery.Services.Kafka;
using Сonfectionery.Services.KSqlDb;

namespace Сonfectionery.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaProducer(this IServiceCollection services,
            Action<KafkaConfig> configAction)
        {
            services.AddSingleton(typeof(IKafkaService<,>), typeof(KafkaService<,>));

            services.Configure(configAction);

            return services;
        }

        public static IServiceCollection AddKSqlDb(this IServiceCollection services,
            Action<KSqlDbConfig> configAction)
        {
            services.AddSingleton(typeof(IKSqlDbService<>), typeof(KSqlDbService<>));

            services.Configure(configAction);

            return services;
        }
    }
}