using System;

namespace Сonfectionery.Infrastructure.Processing.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public DateTime? ProcessedAt { get; private set; }
        public string Type { get; private set; }
        public string Payload { get; private set; }

        private OutboxMessage()
        {
        }

        public static OutboxMessage Create(DateTime occurredOn, string type, string payload)
        {
            if (string.IsNullOrEmpty(payload))
            {
                throw new ArgumentNullException(payload);
            }

            return new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = occurredOn,
                Type = type,
                Payload = payload
            };
        }

        public void RefreshProcessedDate()
        {
            ProcessedAt = DateTime.UtcNow;
        }
    }
}