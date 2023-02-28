using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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

        public ICommand NavigateSetUPCommand { get; }

        public ICommand NavigatePosCommand { get; }

        public ICommand NavigateMainBackGroundCommand { get; }

        public ICommand NavigateUsersAfterSelectionCommand { get; }

        public MainVM(NavigationStore navigationStore)
        {

            _navigationStore = navigationStore;


            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentViewModelChanged;

            /// second change of the CurrentViewModel vy command
            /// the command take the old value from constructor of the mainVM
            /// then command setting the new value to new usersVM
            ///then making update to the new value by firing the event and onproperty changed

            NavigatePosCommand = new NavigatePosCommand( navigationStore);

            NavigateSetUPCommand = new NavigateCommand<SetUpVM>(new NavigationService<SetUpVM>(navigationStore, () => new SetUpVM(_navigationStore)));

            ///تم الاستغناء عن الطرق القديمة لعمل command 
            ///كل واحد لحالة

        }

        private void _navigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
