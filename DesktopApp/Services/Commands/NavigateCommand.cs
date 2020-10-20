using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DesktopApp.Services.Commands
{
    internal class NavigateCommand : BaseCommand
    {
        public NavigateCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
