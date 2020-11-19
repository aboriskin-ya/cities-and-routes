using System;

namespace DesktopApp.Services.Commands
{
    internal class DeleteCityCommand : BaseCommand
    {
        public DeleteCityCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}