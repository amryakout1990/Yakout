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

            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where id=" + 1 + "", connection))
                {
                    table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        _selectedUserStore.SelectedUser.id = 1;
                        _selectedUserStore.SelectedUser.UserName = table.Rows[0][1].ToString();
                        _selectedUserStore.SelectedUser.Password = table.Rows[0][2].ToString();
                        _selectedUserStore.SelectedUser.FullName = table.Rows[0][3].ToString();
                        _selectedUserStore.SelectedUser.JobDes = table.Rows[0][4].ToString();
                        _selectedUserStore.SelectedUser.Email = table.Rows[0][5].ToString();
                        _selectedUserStore.SelectedUser.Phone = table.Rows[0][6].ToString();

                    }
                    else
                    {
                        _selectedUserStore.SelectedUser.id = 0;
                        _selectedUserStore.SelectedUser.UserName = "";
                        _selectedUserStore.SelectedUser.Password = "";
                        _selectedUserStore.SelectedUser.FullName = "";
                        _selectedUserStore.SelectedUser.JobDes = "";
                        _selectedUserStore.SelectedUser.Email = "";
                        _selectedUserStore.SelectedUser.Phone = "";
                    }

                }
            }


            NavigateUsersCommand = new NavigateCommand<UsersVM>(new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore,_selectedUserStore)));

            NavigateMainBackGroundCommand = new NavigateCommand<MainBackGroundVM>(new NavigationService<MainBackGroundVM>(navigationStore, () => new MainBackGroundVM()));

            NavigateItemsCommand = new NavigateCommand<ItemsVM>(new NavigationService<ItemsVM>(navigationStore, () => new ItemsVM(_navigationStore)));

            NavigateCategoriesCommand = new NavigateCommand<CategoriesVM>(new NavigationService<CategoriesVM>(navigationStore, () => new CategoriesVM(navigationStore)));

            NavigatePaymentsCommand = new NavigateCommand<PaymentsVM>(new NavigationService<PaymentsVM>(navigationStore, () => new PaymentsVM(navigationStore)));
        }


    }
}
