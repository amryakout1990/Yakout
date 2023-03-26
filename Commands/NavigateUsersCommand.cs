using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yakout.Stores;
using Yakout.ViewModels;

namespace Yakout.Commands
{
    class NavigateUsersCommand : Utilities.CommandBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly SelectedUserStore _selectedUserStore;

        public NavigateUsersCommand(SelectedUserStore selectedUserStore, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _selectedUserStore = selectedUserStore;


        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new UsersVM(_navigationStore,_selectedUserStore);
        }
        
    }
}
