using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Сonfectionery.Services.Kafka.Producer
{
    public class KafkaProducerService<TKey, TValue> : IDisposable
    {
        private readonly IProducer<TKey, TValue> _producer;
        private readonly string _topic;

        public KafkaProducerService(IProducer<TKey, TValue> producer, string topic)
        {
            _producer = producer;
            _topic = topic;
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