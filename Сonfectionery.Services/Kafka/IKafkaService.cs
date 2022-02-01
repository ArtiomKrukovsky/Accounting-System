using System.Threading.Tasks;

namespace Сonfectionery.Services.Kafka
{
    public interface IKafkaService<TKey, TValue>
    {
        Task ProduceAsync(TKey key, TValue value);
    }
}