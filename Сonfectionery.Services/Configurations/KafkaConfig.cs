using Confluent.Kafka;

namespace Сonfectionery.Services.Configurations
{
    public class KafkaConfig : ProducerConfig
    {
        public static string KafkaConfiguration = "Kafka";

        public string Topic { get; set; }
    }
}