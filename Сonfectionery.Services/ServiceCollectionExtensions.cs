using System;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Сonfectionery.Services.Kafka;

namespace Сonfectionery.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaMessageBus(this IServiceCollection serviceCollection)
            => serviceCollection.AddSingleton(typeof(IKafkaMessageBus<,>), typeof(KafkaMessageBus<,>));

        public static IServiceCollection AddKafkaProducer<TKey, TValue>(this IServiceCollection services,
            Action<KafkaProducerConfig<TKeyey, TValue>> configAction)
        {
            services.AddConfluentKafkaProducer<TKey, TValue>();

            services.AddSingleton<KafkaProducer<TKey, TValue>>();

            services.Configure(configAction);

            return services;
        }

        private static IServiceCollection AddConfluentKafkaProducer<TKey, TValue>(this IServiceCollection services)
        {
            services.AddSingleton(
                sp =>
                {
                    var config = sp.GetRequiredService<IOptions<KafkaProducerConfig<TKey, TValue>>>();

                    var builder = new ProducerBuilder<TKey, TValue>(config.Value).SetValueSerializer(new KafkaSerializer<TValue>());

                    return builder.Build();
                });

            return services;
        }
    }
}