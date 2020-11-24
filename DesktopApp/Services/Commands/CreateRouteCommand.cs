using System;

namespace DesktopApp.Services.Commands
{
    internal class CreateRouteCommand : BaseCommand
    {
        public CreateRouteCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
