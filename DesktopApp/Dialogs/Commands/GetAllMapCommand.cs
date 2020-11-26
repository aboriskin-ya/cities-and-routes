using System;

namespace DesktopApp.Dialogs.Commands
{
    internal class GetAllMapCommand : BaseCommand
    {
        public GetAllMapCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}