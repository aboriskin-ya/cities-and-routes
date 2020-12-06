using System;

namespace DesktopApp.Services.Commands
{
    internal class ResolveTravelSalesmanCommand : BaseCommand
    {
        public ResolveTravelSalesmanCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}
