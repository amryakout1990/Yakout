using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Yakout.Commands;
using Yakout.Models;
using Yakout.Stores;
using Yakout.Utilities;

namespace Yakout.ViewModels
{
    class SetUpVM : Utilities.ViewModelBase
    {
        public ICommand NavigateUsersCommand { get; }

        public ICommand NavigateMainBackGroundCommand { get; }

        public ICommand NavigateItemsCommand { get; }

        public ICommand NavigateCategoriesCommand { get; }

        public ICommand NavigatePaymentsCommand { get; }

        private readonly NavigationStore _navigationStore;

        private SelectedUserStore _selectedUserStore = new SelectedUserStore();

        DataTable table;

        public SetUpVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            NavigateUsersCommand = new NavigateCommand<UsersVM>(new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore)));

            NavigateMainBackGroundCommand = new NavigateCommand<MainBackGroundVM>(new NavigationService<MainBackGroundVM>(navigationStore, () => new MainBackGroundVM()));

            NavigateItemsCommand = new NavigateCommand<ItemsVM>(new NavigationService<ItemsVM>(navigationStore, () => new ItemsVM(_navigationStore)));

            NavigateCategoriesCommand = new NavigateCommand<CategoriesVM>(new NavigationService<CategoriesVM>(navigationStore, () => new CategoriesVM(navigationStore)));

            NavigatePaymentsCommand = new NavigateCommand<PaymentsVM>(new NavigationService<PaymentsVM>(navigationStore, () => new PaymentsVM(navigationStore)));
        }


    }
}
