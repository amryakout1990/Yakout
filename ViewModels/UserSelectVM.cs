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

namespace Yakout.ViewModels
{
    class UserSelectVM : Utilities.ViewModelBase
    {
        

        private readonly NavigationStore _navigationStore;

        private  SelectedUserStore _selectedUserStore = new SelectedUserStore();

        public ICommand NavigateUsersAfterSelectionCommand { get; }

        public ICommand NavigateUsersCommand2 { get; }

        public UserSelectVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            UsersStore usersStore = new UsersStore()
            {
                UserName = "from UserSelectVM",
                Password = "2",
                FullName = "3",
                JobDes = "4",
                Email = "5",
                Phone = "6"
            };

            _selectedUserStore.SelectedUser = usersStore;

            //MessageBox.Show("تم التحديث");
            /// نحتاج الطريقة الاخري مع باراميتر

            ParameterNavigationService<SelectedUserStore, UsersVM> ParameterNavigationService = new ParameterNavigationService<SelectedUserStore, UsersVM>(navigationStore, (_selectedUserStore) => new UsersVM(_navigationStore, _selectedUserStore));

            NavigateUsersAfterSelectionCommand = new NavigateUsersAfterSelectionCommand<UsersVM>(ParameterNavigationService, _selectedUserStore);

            NavigateUsersCommand2 = new NavigateUsersCommand(_navigationStore,_selectedUserStore);

            /// القيمة المخزنة لم تتغير

            // = new NavigateUsersAfterSelectionCommand(_navigationStore, _selectedUserStore);
            MainWindow main = new MainWindow();
            main.DataContext = this;
        }

        public UserSelectVM()
        {
        }



        //public ICommand UserSelectDgCommand { get; }

        //public ICollectionView CollectionView { get; set; }

        //public UserSelectVM(SelectedUserStore _SelectedUserStore,INavigationService navigationService)
        //{
        //    UserSelectDgCommand = new UserSelectDgCommand(this, _SelectedUserStore,navigationService);
        //}

        //public UserSelectVM()
        //{
        //}
    }
}
