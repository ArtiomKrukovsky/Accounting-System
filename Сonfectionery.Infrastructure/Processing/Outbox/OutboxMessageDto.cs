using System;

namespace Сonfectionery.Infrastructure.Processing.Outbox
{
    public class OutboxMessageDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Payload { get; set; }
    }
}