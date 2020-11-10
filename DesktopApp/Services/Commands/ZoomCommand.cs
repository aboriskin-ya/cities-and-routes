using System;

namespace DesktopApp.Services.Commands
{
    internal class ZoomCommand : BaseCommand
    {
        public ZoomCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
