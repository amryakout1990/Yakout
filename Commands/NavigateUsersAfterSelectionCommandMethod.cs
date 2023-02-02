using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Yakout.Utilities;

namespace Yakout.Commands
{
      class NavigateUsersAfterSelectionCommandMethod<Tparameter> : ICommand where Tparameter:class
    {
        private readonly Action<Tparameter> _execute;

        private readonly Predicate<object> _canExecute;

        public NavigateUsersAfterSelectionCommandMethod(Action<Tparameter> execute)
       : this(execute, null)
        {
        }

        public NavigateUsersAfterSelectionCommandMethod(Action<Tparameter> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute((Tparameter)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public void Execute(object parameter)
        {        
            _execute((Tparameter)parameter);
        }
    }

}
 

