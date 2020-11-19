using System;

namespace DesktopApp.Services.Commands
{
    internal class UpdateCityCommand : BaseCommand
    {
        public UpdateCityCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
