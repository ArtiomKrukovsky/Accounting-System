using System.Threading.Tasks;
using Сonfectionery.Services.Kafka.Producer;

namespace Сonfectionery.Services.Kafka
{
    public class KafkaMessageBus<TKey, TValue> : IKafkaMessageBus<TKey, TValue>
    {
        private readonly KafkaProducerService<TKey, TValue> _producer;

        public KafkaMessageBus(KafkaProducerService<TKey, TValue> producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync(TKey key, TValue message)
        {
            await _producer.ProduceAsync(key, message);
        }
    }
}