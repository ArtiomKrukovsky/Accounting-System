using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Сonfectionery.Services.Configurations;
using Сonfectionery.Services.Serializers;

namespace Сonfectionery.Services.Kafka
{
    public class KafkaService<TKey, TValue> : IKafkaService<TKey, TValue> where TValue : class
    {
        private readonly IProducer<TKey, TValue> _producer;
        private readonly string _topic;

        public KafkaService(IOptions<KafkaConfig> config)
        {
            _topic = config.Value.Topic;
            _producer = new ProducerBuilder<TKey, TValue>(config.Value)
                .SetValueSerializer(new KafkaSerializer<TValue>()).Build();
        }

        public async Task ProduceAsync(TKey key, TValue value)
        {
            await _producer.ProduceAsync(_topic, new Message<TKey, TValue> { Key = key, Value = value });
        }

        public void Dispose()
        {
            _producer?.Flush();
            _producer?.Dispose();
        }
    }
}