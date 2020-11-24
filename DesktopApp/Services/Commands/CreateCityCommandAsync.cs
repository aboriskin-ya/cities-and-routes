using System;
using System.Threading.Tasks;

namespace DesktopApp.Services.Commands
{
    internal class CreateCityCommandAsync : BaseCommandAsync
    {
        public CreateCityCommandAsync(Predicate<object> CanExecute, Func<object, Task> Executed) : base(CanExecute, Executed)
        {
        }
    }
}