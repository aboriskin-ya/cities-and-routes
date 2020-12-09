using System;

namespace DesktopApp.Services.Commands
{
    internal class PathResolverCancelCommand : BaseCommand
    {
        public PathResolverCancelCommand(Predicate<object> CanExecute, Action<object> Executed) : base(CanExecute, Executed)
        {
        }
    }
}