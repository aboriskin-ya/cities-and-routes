using System;

namespace DesktopApp.Services.Commands
{
    internal class CancelCreatingRouteCommand : BaseCommand
    {
        public CancelCreatingRouteCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}