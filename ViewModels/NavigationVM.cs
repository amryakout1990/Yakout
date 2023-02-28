using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Yakout.Models;
using Yakout.Stores;
using Yakout.Utilities;

namespace Yakout.ViewModels
{
    class NavigationVM : Utilities.ViewModelBase
    {

        /// <summary>
        /// استخدام طريقة الاخ الهندي ب relaycommand 
        /// يحتاج الغاء datacontex
        /// الموجود في app constructor
        /// بتاع سينجلتون عشان يشتغل
        /// و اغير اسماء commands 
        /// المعمولها binding
        /// </summary>
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged("CurrentView"); }
        }

        public NavigationVM()
        {
            CurrentView = new MainBackGroundVM();
        }

        ///Users Page after selection///

        private void Users2(object ooo)
        {

            SelectedUserStore selectedUserStore = new SelectedUserStore();

            //CurrentView = new UsersVM();
        }
        private ICommand _UsersCommand2;

        public ICommand UsersCommand2
        {
            get
            {
                if (_UsersCommand2 == null)
                {
                    _UsersCommand2 = new RelayCommand(Users2);
                }
                return _UsersCommand2;
            }
            set { _UsersCommand2 = value; OnPropertyChanged(); }
        }


        ///MainBackGround Page/////

        private void MainBackGround(object obj) => CurrentView = new MainBackGroundVM();

        private ICommand _MainBackGroundCommand;

        public ICommand MainBackGroundCommand
        {
            get
            {
                if (_MainBackGroundCommand == null)
                {
                    _MainBackGroundCommand = new RelayCommand(MainBackGround);
                }
                return _MainBackGroundCommand;
            }
            set { _MainBackGroundCommand = value; OnPropertyChanged(); }
        }

        ///options page /////

        private void Options(object obj) => CurrentView = new OptionsVM();

        private ICommand _OptionsCommand;

        public ICommand OptionsCommand
        {
            get
            {
                if (_OptionsCommand == null)
                {
                    _OptionsCommand = new RelayCommand(Options);
                }
                return _OptionsCommand;
            }
            set { _OptionsCommand = value; OnPropertyChanged(); }
        }

        ///SetUp page /////

        //private void SetUp(object obj) => CurrentView = new SetUpVM();

        //private ICommand _SetUpCommand;

        //public ICommand SetUpCommand
        //{
        //    get
        //    {
        //        if (_SetUpCommand == null)
        //        {
        //            _SetUpCommand = new RelayCommand(SetUp);
        //        }
        //        return _SetUpCommand;
        //    }
        //    set { _SetUpCommand = value; OnPropertyChanged(); }
        //}

        ///Pos Page///
        //private void Pos(object ooo) => CurrentView = new PosVM();

        //private ICommand _PosCommand;

        //public ICommand PosCommand
        //{
        //    get
        //    {
        //        if (_PosCommand == null)
        //        {
        //            _PosCommand = new RelayCommand(Pos);
        //        }
        //        return _PosCommand;
        //    }
        //    set { _PosCommand = value; OnPropertyChanged(); }
        //}

        ///Reports///
        private void Reports(object ooo) => CurrentView = new ReportsVM();

        private ICommand _ReportsCommand;

        public ICommand ReportsCommand
        {
            get
            {
                if (_ReportsCommand == null)
                {
                    _ReportsCommand = new RelayCommand(Reports);
                }
                return _ReportsCommand;
            }
            set { _ReportsCommand = value; OnPropertyChanged(); }
        }

        internal void Execute(object users2)
        {
            throw new NotImplementedException();
        }

        ///Users Page///
        private void Users(object ooo)
        {
            //CurrentView = new UsersVM();
        }
        private ICommand _UsersCommand;

        public ICommand UsersCommand
        {
            get
            {
                if (_UsersCommand == null)
                {
                    _UsersCommand = new RelayCommand(Users);
                }
                return _UsersCommand;
            }
            set { _UsersCommand = value; OnPropertyChanged(); }
        }


        ///Users Select Page///
        //private void UserSelect(object ooo) => CurrentView = new UserSelectVM();

        //private ICommand _UserSelectCommand;

        //public ICommand UserSelectCommand
        //{
        //    get
        //    {
        //        if (_UserSelectCommand == null)
        //        {
        //            _UserSelectCommand = new RelayCommand(UserSelect);
        //        }
        //        return _UserSelectCommand;
        //    }
        //    set { _UserSelectCommand = value; OnPropertyChanged(); }
        //}

        ///test///
        private void test(object ooo) => MessageBox.Show("Test");

        private ICommand _testCommand;

        public ICommand testCommand
        {
            get
            {
                if (_testCommand == null)
                {
                    _testCommand = new RelayCommand(test);
                }
                return _testCommand;
            }
            set { _testCommand = value; OnPropertyChanged(); }
        }

    }
}
