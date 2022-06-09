using MediatR;
using System;

namespace Сonfectionery.Domain.Seedwork
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}