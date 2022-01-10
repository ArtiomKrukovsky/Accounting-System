using System.Threading.Tasks;

namespace Сonfectionery.Services.Kafka
{
    public interface IKafkaMessageBus<in TKey, in TValue> where TValue : class
    {
        Task PublishAsync(TKey key, TValue message);
    }
}