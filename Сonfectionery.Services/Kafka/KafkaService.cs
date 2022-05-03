using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Сonfectionery.Services.Configurations;
using Сonfectionery.Services.Serializers;

namespace Сonfectionery.Services.Kafka
{
    public class KafkaService<TValue> : IKafkaService<TValue> where TValue : class
    {
        private readonly IProducer<string, TValue> _producer;
        private readonly string _topic;

        public KafkaService(IOptions<KafkaConfig> config)
        {
            _topic = config.Value.Topic;
            _producer = new ProducerBuilder<string, TValue>(config.Value)
                .SetValueSerializer(new KafkaSerializer<TValue>()).Build();
        }

        public async Task ProduceAsync(string topic, string key, TValue value)
        {
            await _producer.ProduceAsync(topic, new Message<string, TValue> { Key = key, Value = value });
        }

        public void Dispose()
        {
            _producer?.Flush();
            _producer?.Dispose();
        }
    }
}