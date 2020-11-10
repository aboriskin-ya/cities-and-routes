using System;

namespace DesktopApp.Services.Commands
{
    internal class ShowCreateMapDialogCommand : BaseCommand
    {
        public ShowCreateMapDialogCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
