using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Сonfectionery.Services.Kafka.Serializers;

namespace Сonfectionery.Services.Kafka.Producer
{
    public class KafkaProducerService<TKey, TValue> : IDisposable
    {
        private readonly IProducer<TKey, TValue> _producer;
        private readonly string _topic;

        public KafkaProducerService(IOptions<KafkaProducerConfig> config)
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