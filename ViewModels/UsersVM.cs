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
using Yakout.Views;

namespace Yakout.ViewModels
{
    class UsersVM : Utilities.ViewModelBase
    {
        /// <summary>
        /// الكود دا مش شغال و دي حاجة غريبة
        /// اشتغل بالطريقة التانية المجمعه في
        /// navigationCommand
        /// </summary>
        public ICommand NavigateUsersSelectCommand { get; }

        public  SelectedUserStore _selectedUserStore;

        //public int Id => _selectedUserStore.SelectedUser?.id;

        private readonly NavigationStore _navigationStore;
        public ICommand NavigateMainBackGroundCommand { get; }

        public UsersVM(NavigationStore navigationStore,SelectedUserStore selectedUserStore)
        {
            _navigationStore = navigationStore;
            _selectedUserStore = selectedUserStore;
            NavigateMainBackGroundCommand = new NavigateCommand<MainBackGroundVM>(new NavigationService<MainBackGroundVM>(navigationStore, () => new MainBackGroundVM()));

            NavigateUsersSelectCommand = new NavigateCommand<UserSelectVM>(new NavigationService<UserSelectVM>(navigationStore, () => new UserSelectVM(_navigationStore)));

            _selectedUserStore.SelectedUserChanged += _selectedUserStore_SelectedUserChanged;
            //show();
        }
        public string UserName => _selectedUserStore.SelectedUser?.UserName;

        public string Password => _selectedUserStore.SelectedUser?.Password;

        public string FullName => _selectedUserStore.SelectedUser?.FullName;

        public string JobDes => _selectedUserStore.SelectedUser?.JobDes;

        public string Email => _selectedUserStore.SelectedUser?.Email;

        public string Phone => _selectedUserStore.SelectedUser?.Phone;


        private void _selectedUserStore_SelectedUserChanged()
        {
            //OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(FullName));
            OnPropertyChanged(nameof(JobDes));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Phone));
        }

        public override void Dispose()
        {
            _selectedUserStore.SelectedUserChanged -= _selectedUserStore_SelectedUserChanged;

            base.Dispose();
        }
        private void show()
        {
            MessageBox.Show(Password);
        }


        //DataTable table;

        //public UsersVM()
        //{
        //    table = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
        //    {
        //        using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where id=" + 3 + "", connection))
        //        {
        //            table = new DataTable();
        //            adapter.Fill(table);
        //            if (table.Rows.Count > 0)
        //            {
        //                UserName = table.Rows[0][1].ToString();
        //                Password = table.Rows[0][2].ToString();
        //                FullName = table.Rows[0][3].ToString();
        //                JobDes = table.Rows[0][4].ToString();
        //                Email = table.Rows[0][5].ToString();
        //                Phone = table.Rows[0][6].ToString();
        //            }

        //        }
        //    }

        //}

        //public UsersVM(SelectedUserStore _selectedUserStore)
        //{
        //    selectedUserStore = _selectedUserStore;

        //    table = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
        //    {
        //        using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where id=" + 3 + "", connection))
        //        {
        //            table = new DataTable();
        //            adapter.Fill(table);
        //            if (table.Rows.Count > 0)
        //            {
        //                UserName = selectedUserStore.SelectedUser.UserName;
        //                Password = selectedUserStore.SelectedUser.Password;
        //                FullName = selectedUserStore.SelectedUser.FullName;
        //                JobDes = selectedUserStore.SelectedUser.JobDes;
        //                Email = selectedUserStore.SelectedUser.Email;
        //                Phone = selectedUserStore.SelectedUser.Phone;
        //            }

        //        }
        //    }

        //}

    }
}
