using Confluent.Kafka;

namespace Сonfectionery.Services.Kafka.Producer
{
    public class KafkaProducerConfig : ProducerConfig
    {
        public string Topic { get; set; }
    }
}