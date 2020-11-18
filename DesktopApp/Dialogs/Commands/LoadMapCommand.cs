using System;

namespace DesktopApp.Dialogs.Commands
{
    internal class LoadMapCommand : BaseCommand
    {
        public LoadMapCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}