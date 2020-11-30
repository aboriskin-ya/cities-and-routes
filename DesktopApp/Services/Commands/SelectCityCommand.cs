using System;

namespace DesktopApp.Services.Commands
{
    internal class SelectCityCommand : BaseCommand
    {
        public SelectCityCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
