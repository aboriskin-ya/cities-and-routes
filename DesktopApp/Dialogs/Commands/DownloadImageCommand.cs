using System;

namespace DesktopApp.Dialogs.Commands
{
    internal class DownloadImageCommand : BaseCommand
    {
        public DownloadImageCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
