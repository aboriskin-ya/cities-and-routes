using System;

namespace DesktopApp.Services.Commands
{
    internal class DeleteRouteCommand : BaseCommand
    {
        public DeleteRouteCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}