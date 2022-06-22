using System.Threading.Tasks;

namespace Сonfectionery.Infrastructure.Processing.EventsDispatcher.Interfaces
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync(СonfectioneryContext context);
    }
}