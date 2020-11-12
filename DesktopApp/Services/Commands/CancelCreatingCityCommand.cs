using System;

namespace DesktopApp.Services.Commands
{
    internal class CancelCreatingCityCommand : BaseCommand
    {
        public CancelCreatingCityCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}