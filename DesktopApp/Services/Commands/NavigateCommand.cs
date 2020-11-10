using System;

namespace DesktopApp.Services.Commands
{
    internal class NavigateCommand : BaseCommand
    {
        public NavigateCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
