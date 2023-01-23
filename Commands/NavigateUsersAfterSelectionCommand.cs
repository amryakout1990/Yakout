using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yakout.Models;
using Yakout.Stores;
using Yakout.Utilities;
using Yakout.ViewModels;

namespace Yakout.Commands
{
    class NavigateUsersAfterSelectionCommand<TViewModel> : Utilities.CommandBase
    where TViewModel : Utilities.ViewModelBase
    {

        private readonly ParameterNavigationService<SelectedUserStore, UsersVM> _navigationService;

        private readonly SelectedUserStore _selectedUserStore = new SelectedUserStore();

        public NavigateUsersAfterSelectionCommand(ParameterNavigationService<SelectedUserStore, UsersVM> navigationService,SelectedUserStore selectedUserStore)
        {
            _navigationService = navigationService;
            _selectedUserStore = selectedUserStore;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate(_selectedUserStore);
           MessageBox.Show("Test");
        }
    }

}
