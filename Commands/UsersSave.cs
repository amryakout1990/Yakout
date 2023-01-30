using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yakout.ViewModels;

namespace Yakout.Commands
{
    class UsersSave : ICommand
    {
        private readonly UsersVM usersVM;

        private readonly Action<object> _execute;

        private readonly Func<object, bool> _canExecute;

        public UsersSave(UsersVM usersVM,Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.usersVM = usersVM;
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
