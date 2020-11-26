using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApp.Services.Commands
{
    public class RelayCommandAsync : ICommand
    {
        private Predicate<object> canExecute;
        private Func<object, Task> execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommandAsync(Predicate<object> CanExecute, Func<object, Task> Executed)
        {
            execute = Executed;
            canExecute = CanExecute;
        }
        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        public virtual async Task ExecuteAsync(object parameter) => await execute.Invoke(parameter);

        public void Execute(object parameter)
        {
            ExecuteAsync(parameter).RunSynchronously();
        }
    }
}