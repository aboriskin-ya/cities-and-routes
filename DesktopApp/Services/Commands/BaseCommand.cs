using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.Services.Commands
{
    internal class BaseCommand : ICommand
    {
        private Predicate<object> _CanExecute;
        private Action<object> _Executed;
        internal BaseCommand(Predicate<object> CanExecute, Action<Object> Executed)
        {
            _Executed = Executed;
            _CanExecute = CanExecute;
        }
        

        
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;
        

        public void Execute(object parameter)
        {
            _Executed?.Invoke(parameter);
        }
    }
}
