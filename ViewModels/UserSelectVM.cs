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

        public NavigateUsersAfterSelectionCommandMethod<object, UsersVM> NavigateUsersAfterSelectionCommandMethod { get; }

        public ICommand UsersFirstButtonCommand => new UsersButtonsCommand(getDataFirst);

        private DataTable _table_users;

        public DataTable table_users
        {
            get { return _table_users; }
            set { _table_users = value; OnPropertyChanged(); }
        }

        private DataRowView _selected_User;

        public DataRowView Selected_User
        {
            get { return _selected_User; }
            set { _selected_User = value; }
        }

        private DataTable _myUser;

        public DataTable myUser
        {
            get { return _myUser; }
            set { _myUser = value; OnPropertyChanged(); }
        }

        //private readonly SelectedUserStore _selectedUserStore = new SelectedUserStore();

        private SelectedUserStore selectedUserStore;

        public SelectedUserStore _selectedUserStore
        {
            get
            {
                if (selectedUserStore == null)
                {
                    selectedUserStore = new SelectedUserStore();
                }
                return selectedUserStore;
            }
            set
            {
                selectedUserStore = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateUsersAfterSelectionCommand { get; private set; }

        public ICommand NavigateUsersCommandBack { get; private set; }

        public ICommand NavigateUsersAfterSelection { get; private set; }

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

            NavigateUsersCommandBack = new NavigateUsersAfterSelectionCommandMethod<object, UsersVM>((_selectedUserStore) => { getDataFirst(_selectedUserStore); }, new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore, _selectedUserStore)));

            NavigateUsersAfterSelectionCommandMethod = new NavigateUsersAfterSelectionCommandMethod<object, UsersVM>((_selectedUserStore) => { OnItemSelectionChanged(_selectedUserStore); }, new NavigationService<UsersVM>(navigationStore, () => new UsersVM(_navigationStore, _selectedUserStore)));

            NavigateUsersAfterSelection = new ActionCommand(GetUser);

            _selectedUserStore_SelectedUserChanged();

        }

        private void GetUser()
        {
            if (Selected_User!=null)
            {
            _selectedUserStore.SelectedUser.UserName = Selected_User[1].ToString();
            _selectedUserStore.SelectedUser.Password = Selected_User[2].ToString();
            _selectedUserStore.SelectedUser.FullName = Selected_User[3].ToString();
            _selectedUserStore.SelectedUser.JobDes = Selected_User[4].ToString();
            _selectedUserStore.SelectedUser.Email = Selected_User[5].ToString();
            _selectedUserStore.SelectedUser.Phone = Selected_User[6].ToString();
            _selectedUserStore_SelectedUserChanged();
            _navigationStore.CurrentViewModel = new UsersVM(_navigationStore,_selectedUserStore);
            Dispose();
            }
            else
            {
                MessageBox.Show("empty");
            }
        }

        private void _selectedUserStore_SelectedUserChanged()
        {
            OnPropertyChanged(nameof(_selectedUserStore.SelectedUser. UserName));
            OnPropertyChanged(nameof(_selectedUserStore.SelectedUser.Password));
            OnPropertyChanged(nameof(_selectedUserStore.SelectedUser.FullName));
            OnPropertyChanged(nameof(_selectedUserStore.SelectedUser.JobDes));
            OnPropertyChanged(nameof(_selectedUserStore.SelectedUser.Email));
            OnPropertyChanged(nameof(_selectedUserStore.SelectedUser.Phone));
        }
        public override void Dispose()
        {
            _selectedUserStore.SelectedUserChanged -= _selectedUserStore_SelectedUserChanged;
            base.Dispose();
        }

        private void OnItemSelectionChanged(object obj)
        {
            try
            {
                DataRowView _obj = (DataRowView)obj;
                UsersStore usersStore = new UsersStore()
                {
                    UserName = _obj[1].ToString(),
                    Password = _obj[2].ToString(),
                    FullName = _obj[3].ToString(),
                    JobDes = _obj[4].ToString(),
                    Email = _obj[5].ToString(),
                    Phone = _obj[6].ToString()
                };
                _selectedUserStore.SelectedUser = usersStore;

            }
            catch (Exception rr)
            {
                MessageBox.Show(rr+"");
            }
           

            //if (obj is DataRowView _obj)
            //{
            //}

        }

        private void getDataFirst(object ooo)
        {
            _table_users = new DataTable();
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where id=" + 1 + "", connection))
                {
                    _table_users = new DataTable();
                    adapter.Fill(_table_users);
                    if (_table_users.Rows.Count > 0)
                    {
                        UsersStore usersStore = new UsersStore()
                        {
                            UserName = _table_users.Rows[0][1].ToString(),
                            Password = _table_users.Rows[0][2].ToString(),
                            FullName = _table_users.Rows[0][3].ToString(),
                            JobDes = _table_users.Rows[0][4].ToString(),
                            Email = _table_users.Rows[0][5].ToString(),
                            Phone = _table_users.Rows[0][6].ToString()
                        };
                        _selectedUserStore.SelectedUser = usersStore;

                    }

                }
            }
        }


    }
}
