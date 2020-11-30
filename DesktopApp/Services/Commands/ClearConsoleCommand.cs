using System;

namespace DesktopApp.Services.Commands
{
    internal class ClearConsoleCommand : BaseCommand
    {
        public ClearConsoleCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
