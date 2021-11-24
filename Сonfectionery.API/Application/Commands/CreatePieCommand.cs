using Сonfectionery.API.Application.Interfaces;

namespace Сonfectionery.API.Application.Commands
{
    public class CreatePieCommand : ICommand<bool>
    {
        public int MyProperty { get; set; }
    }
}
