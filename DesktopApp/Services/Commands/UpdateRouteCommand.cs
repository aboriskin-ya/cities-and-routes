using System;

namespace DesktopApp.Services.Commands
{
    internal class UpdateRouteCommand : BaseCommand
    {
        public UpdateRouteCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
