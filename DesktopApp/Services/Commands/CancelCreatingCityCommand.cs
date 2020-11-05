using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.Services.Commands
{
    internal class CancelCreatingCityCommand : BaseCommand
    {
        public CancelCreatingCityCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
