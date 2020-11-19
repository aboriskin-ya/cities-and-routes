using System;

namespace DesktopApp.Dialogs.Commands
{
    internal class DeleteMapCommand : BaseCommand
    {
        public DeleteMapCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}