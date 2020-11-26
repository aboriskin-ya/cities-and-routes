using System;

namespace DesktopApp.Dialogs.Commands
{
    internal class CreateMapCommand : BaseCommand
    {
        public CreateMapCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}