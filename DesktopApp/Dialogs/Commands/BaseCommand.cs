﻿using System;
using System.Windows.Input;

namespace DesktopApp.Dialogs.Commands
{
    internal abstract class BaseCommand : ICommand
    {
        private Predicate<object> _CanExecute;
        private Action<object> _Executed;
        internal BaseCommand(Predicate<object> CanExecute, Action<object> Executed)
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


        public virtual void Execute(object parameter) => _Executed.Invoke(parameter);
    }
}
