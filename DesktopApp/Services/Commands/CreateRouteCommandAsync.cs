using System;
using System.Threading.Tasks;

namespace DesktopApp.Services.Commands
{
    internal class CreateRouteCommandAsync : BaseCommandAsync
    {
        public CreateRouteCommandAsync(Predicate<object> CanExecute, Func<object, Task> Executed) : base(CanExecute, Executed)
        {
        }
    }
}