using Confluent.Kafka;

namespace Сonfectionery.Services.Kafka.Configurations
{
    public class KafkaProducerConfig : ProducerConfig
    {
        public static string KafkaConfiguration = "Kafka";

        public string Topic { get; set; }
    }
}