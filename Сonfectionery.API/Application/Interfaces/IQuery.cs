using MediatR;

namespace Сonfectionery.API.Application.Interfaces
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
