﻿using System;
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

        private SelectedStore selectedStore;

        private readonly SelectedItemStore _selectedItemStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand NavigateUsersCommand { get; }
        public ICommand NavigateSetUPCommand { get; }
        public ICommand NavigatePosCommand { get; }
        public ICommand ReportsCommand { get; private set; }
        public ICommand NavigateMainBackGroundCommand { get; }
        public ICommand NavigateUsersAfterSelectionCommand { get; }
        public ICommand OptionsCommand { get; private set; }

        public MainVM(NavigationStore navigationStore)
        {

            _navigationStore = navigationStore;

            _selectedItemStore = new SelectedItemStore();

            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentViewModelChanged;

            /// second change of the CurrentViewModel vy command
            /// the command take the old value from constructor of the mainVM
            /// then command setting the new value to new usersVM
            ///then making update to the new value by firing the event and onproperty changed
        
            NavigatePosCommand = new NavigatePosCommand( navigationStore, _selectedItemStore);

            NavigateSetUPCommand = new NavigateCommand<SetUpVM>(new NavigationService<SetUpVM>(navigationStore, () => new SetUpVM(_navigationStore)));

            ///تم الاستغناء عن الطرق القديمة لعمل command 
            ///كل واحد لحالة
            ReportsCommand = new ActionCommand(reports);
            OptionsCommand = new ActionCommand(option);
        }

        private void option()
        {
            _navigationStore.CurrentViewModel = new OptionsVM(_navigationStore);
        }

        private void _navigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        private void reports()
        {
            _navigationStore.CurrentViewModel=new PrintInvoiceVM();
        }

    }
}
