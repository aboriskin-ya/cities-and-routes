using System;

namespace DesktopApp.Services.Commands
{
    internal class AddingCitiesRoutesOpenCommand : BaseCommand
    {
        public AddingCitiesRoutesOpenCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}