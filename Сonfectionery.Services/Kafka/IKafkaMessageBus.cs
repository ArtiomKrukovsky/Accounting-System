using System.Threading.Tasks;

namespace Сonfectionery.Services.Kafka
{
    public interface IKafkaMessageBus<TKey, TValue>
    {
        Task PublishAsync(TKey key, TValue message);
    }
}