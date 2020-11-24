using System;

namespace DesktopApp.Services.Commands
{
    internal class ColculateShortestPathCommand : BaseCommand
    {
        public ColculateShortestPathCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}