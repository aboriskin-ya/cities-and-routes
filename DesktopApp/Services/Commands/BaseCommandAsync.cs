using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApp.Services.Commands
{
    internal abstract class BaseCommandAsync : ICommand
    {
        private Predicate<object> _CanExecute;
        private Func<object, Task> _Executed;
        internal BaseCommandAsync(Predicate<object> CanExecute, Func<object, Task> Executed)
        {
            _Executed = Executed;
            _CanExecute = CanExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public virtual bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;


        public virtual async Task ExecuteAsync(object parameter) => await _Executed.Invoke(parameter);

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync(parameter).RunSynchronously();
        }
    }
}