using System;

namespace DesktopApp.Services.Commands
{
    internal class CreateCityCommand : BaseCommand
    {
        public CreateCityCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
