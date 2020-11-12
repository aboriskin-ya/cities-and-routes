using System;

namespace DesktopApp.Services.Commands
{
    internal class AddNewRouteCommand : BaseCommand
    {
        public AddNewRouteCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
