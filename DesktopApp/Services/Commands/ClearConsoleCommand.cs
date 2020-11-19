using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.Services.Commands
{
    internal class ClearConsoleCommand : BaseCommand
    {
        public ClearConsoleCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
