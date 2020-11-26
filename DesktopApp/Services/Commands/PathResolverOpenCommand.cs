using System;

namespace DesktopApp.Services.Commands
{
    internal class PathResolverOpenCommand : BaseCommand
    {
        public PathResolverOpenCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}