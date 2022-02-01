using System;
using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Сonfectionery.Services.Serializers
{
    public class KafkaSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (typeof(T) == typeof(Null))
                return null;

            if (typeof(T) == typeof(Ignore))
                throw new NotSupportedException("Not Supported.");

            var formatSettings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd''T''HH:mm:ss.fff"
            };

            var json = JsonConvert.SerializeObject(data, formatSettings);

            return Encoding.UTF8.GetBytes(json);
        }
    }
}