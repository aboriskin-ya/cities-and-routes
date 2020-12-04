using System;

namespace DesktopApp.Services.Commands
{
    internal class CancelCitiesSelectingCommand : BaseCommand
    {
        public CancelCitiesSelectingCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
