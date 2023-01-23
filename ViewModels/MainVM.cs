using System;
using System.Collections.Generic;
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
    class MainVM :Utilities.ViewModelBase
    {
        ///اللعب كله هنا

        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand NavigateUsersCommand { get; }

        public ICommand NavigatePosCommand { get; }

        private SelectedUserStore _selectedUserStore = new SelectedUserStore();

        private UsersStore _usersStore = new UsersStore()
        {
                UserName = "from MainVM",
                Password = "2",
                FullName = "3",
                JobDes = "4",
                Email = "5",
                Phone = "6"
        };

        //public ICommand NavigateUsersSelectCommand { get; }

        //public ICommand NavigateMainBackGroundCommand { get; }

        public ICommand NavigateUsersAfterSelectionCommand { get; }

        public MainVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _selectedUserStore.SelectedUser = _usersStore;

            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentViewModelChanged;

            /// second change of the CurrentViewModel vy command
            /// the command take the old value from constructor of the mainVM
            /// then command setting the new value to new usersVM
            ///then making update to the new value by firing the event and onproperty changed
            
            NavigateUsersCommand = new NavigateUsersCommand(navigationStore, _selectedUserStore);

            NavigatePosCommand = new NavigatePosCommand( navigationStore);

            //NavigateUsersSelectCommand = new NavigateUsersSelectCommand(navigationStore);

            //NavigateUsersAfterSelectionCommand = new NavigateCommand<UserSelectVM>(new NavigationService<UserSelectVM>(navigationStore, () => new UserSelectVM(_navigationStore)));

            //NavigateUsersAfterSelectionCommand = new NavigateUsersAfterSelectionCommand(_navigationStore, _selectedUserStore);


            //NavigateMainBackGroundCommand = new NavigateCommand <MainBackGroundVM>(new NavigationService<MainBackGroundVM>(navigationStore,()=>new MainBackGroundVM()));

            ///تم الاستغناء عن الطرق القديمة لعمل command 
            ///كل واحد لحالة
        }

        private void _navigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
