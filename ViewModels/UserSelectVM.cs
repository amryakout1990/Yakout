using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yakout.Commands;
using Yakout.Models;
using Yakout.Stores;
using System.ComponentModel;
using Yakout.Utilities;
using System.Windows;
using Yakout.Views;
using System.Data;
using System.Data.SqlClient;

namespace Yakout.ViewModels
{
    class UserSelectVM : Utilities.ViewModelBase
    {
        private DataTable table_users;

        private readonly NavigationStore _navigationStore;

        private  SelectedUserStore SelectedUserStore ;

        public ICommand NavigateUsersAfterSelectionCommand { get; }

        public ICommand NavigateUsersCommand2 { get; }

        public UserSelectVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users", connection))
                {
                    table_users = new DataTable();
                    adapter.Fill(table_users);
                }
            }


            UsersStore usersStore = new UsersStore()
            {
                UserName = "from UserSelectVM",
                Password = "2",
                FullName = "3",
                JobDes = "4",
                Email = "5",
                Phone = "6"
            };

            //_selectedUserStore.SelectedUser = usersStore;

            //NavigateUsersAfterSelectionCommand = new NavigateCommand<UsersVM>(new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore,_selectedUserStore)));

            //NavigateUsersCommand2 = new NavigateCommand<UsersVM>(new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore, _selectedUserStore)));
            
        }

    }
}
