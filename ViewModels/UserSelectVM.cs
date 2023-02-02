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
        private  NavigationStore _navigationStore;

        public NavigateUsersAfterSelectionCommandMethod<object> SelectedUserStoreChanged { get; private set; }

        private DataTable _table_users;

        public DataTable table_users
        {
            get { return _table_users; }
            set { _table_users = value; OnPropertyChanged(); }
        }

        private DataTable _myUser;

        public DataTable myUser
        {
            get { return _myUser; }
            set { _myUser = value; OnPropertyChanged(); }
        }

        private readonly SelectedUserStore _selectedUserStore = new SelectedUserStore();

        public ICommand NavigateUsersAfterSelectionCommand;

        public ICommand NavigateUsersCommandBack { get; }

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
            //if (myUser.Rows.Count > 0)
            //{
            //    _selectedUserStore.SelectedUser = getDataRow(myUser);
            //}
            //else
            //{

            //}

            UsersStore usersStore1 = new UsersStore()
            {

                UserName = "gg",
                Password = "gg",
                FullName = "gg",
                JobDes = "ggg",
                Email = "gg",
                Phone = "gg"
            };

            _selectedUserStore.SelectedUser = usersStore1;

            NavigateUsersAfterSelectionCommand = new NavigateCommand<UsersVM>(new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore, _selectedUserStore)));

            NavigateUsersCommandBack = new NavigateCommand<UsersVM>(new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore, _selectedUserStore)));
           
            SelectedUserStoreChanged = new NavigateUsersAfterSelectionCommandMethod<object>((obj) => { OnItemSelectionChanged(obj); });


        }

        private void OnItemSelectionChanged(object obj)
        {
                MessageBox.Show("Test1");
            if (obj is DataRowView _obj)
            {
                MessageBox.Show("Test2");
                SelectedUserStore _selectedUserStore = new SelectedUserStore();
                _selectedUserStore.SelectedUser = getDataRow(_obj);
                MessageBox.Show(_selectedUserStore.SelectedUser.UserName);
                NavigateUsersAfterSelectionCommand = new NavigateCommand<UsersVM>(new NavigationService<UsersVM>(_navigationStore, () => new UsersVM(_navigationStore, _selectedUserStore)));
            }

        }

        private UsersStore getDataRow(DataRowView dataRow)
        {
            UsersStore usersStore = new UsersStore()
            {
                
                UserName = dataRow[1].ToString(),
                Password = dataRow[2].ToString(),
                FullName = dataRow[3].ToString(),
                JobDes = dataRow[4].ToString(),
                Email = dataRow[5].ToString(),
                Phone = dataRow[6].ToString()
            };
            return usersStore;
        }
    }
}
