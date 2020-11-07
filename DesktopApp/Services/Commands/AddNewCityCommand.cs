using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.Services.Commands
{
    internal class AddNewCityCommand : BaseCommand
    {
        public AddNewCityCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
