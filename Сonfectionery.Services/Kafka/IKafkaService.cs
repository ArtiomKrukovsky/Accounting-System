using System.Threading.Tasks;

namespace Сonfectionery.Services.Kafka
{
    public interface IKafkaService<TValue>
    {
        Task ProduceAsync(string topic, string key, TValue value);
    }
}