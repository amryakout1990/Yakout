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

            table = new DataTable();
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where id=" +1+ "", connection))
                {
                    table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        UsersStore _usersStore = new UsersStore()
                        {
                            UserName = table.Rows[0][1].ToString(),
                            Password = table.Rows[0][2].ToString(),
                            FullName = table.Rows[0][3].ToString(),
                            JobDes = table.Rows[0][4].ToString(),
                            Email = table.Rows[0][5].ToString(),
                            Phone = table.Rows[0][6].ToString()
                        };
                        _selectedUserStore.SelectedUser = _usersStore;

                        NavigateUsersCommand = new NavigateCommand<UsersVM>(new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore, _selectedUserStore)));

                    }
                    else
                    {
                        UsersStore _usersStore = new UsersStore()
                        {
                            UserName = "",
                            Password = "",
                            FullName = "",
                            JobDes = "",
                            Email = "",
                            Phone = ""
                        };

                        _selectedUserStore.SelectedUser = _usersStore;

                        NavigateUsersCommand = new NavigateCommand<UsersVM>(new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore, _selectedUserStore)));

                    }

                }
            }


           
            NavigateMainBackGroundCommand = new NavigateCommand<MainBackGroundVM>(new NavigationService<MainBackGroundVM>(navigationStore, () => new MainBackGroundVM()));

            NavigateItemsCommand = new NavigateCommand<ItemsVM>(new NavigationService<ItemsVM>(navigationStore, () => new ItemsVM()));

            NavigateCategoriesCommand = new NavigateCommand<CategoriesVM>(new NavigationService<CategoriesVM>(navigationStore, () => new CategoriesVM(navigationStore)));

            NavigatePaymentsCommand = new NavigateCommand<PaymentsVM>(new NavigationService<PaymentsVM>(navigationStore, () => new PaymentsVM(navigationStore)));
        }


    }
}
