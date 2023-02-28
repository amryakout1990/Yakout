using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using Yakout.Commands;
using Yakout.Models;
using Yakout.Stores;
using Yakout.Utilities;

namespace Yakout.ViewModels
{
    class PosVM : Utilities.ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// دا اللي مربوط في الفيوووو الرئيسي
        /// </summary>
        
        public ViewModelBase CurrentMenuButtons => _navigationStore.CurrentMenuButtons;

        private string _qty;

        public string Qty
        {
            get { return _qty; }
            set { _qty = value; OnPropertyChanged(nameof(Qty)); }
        }

        private DataTable _table;

        public DataTable Table
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(nameof(Table)); }
        }
        private SqlCommand _command;

        public SqlCommand Command
        {
            get { return _command; }
            set { _command = value; OnPropertyChanged(nameof(Command)); }
        }
        private string _categoryId;

        public string CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; OnPropertyChanged(); }
        }

        private SelectedPropertyStore _SelectedPropertyStore { get; }

        private DataView _GridDataView;

        public DataView GridDataView
        {
            get { return _SelectedPropertyStore.GridDataView; }
        }


        public ICommand Calculator1Command { get; private set; }
        public ICommand Calculator2Command { get; private set; }
        public ICommand Calculator3Command { get; private set; }
        public ICommand Calculator4Command { get; private set; }
        public ICommand Calculator5Command { get; private set; }
        public ICommand Calculator6Command { get; private set; }
        public ICommand Calculator7Command { get; private set; }
        public ICommand Calculator8Command { get; private set; }
        public ICommand Calculator9Command { get; private set; }
        public ICommand Calculator0Command { get; private set; }
        public ICommand CalculatorClearCommand { get; private set; }
        public ICommand CalculatorPointCommand { get; private set; }


        public PosVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentMenuButtons = new ButtonCategoriesVM(_navigationStore);
            Qty = "0";
            Calculator1Command = new ActionCommand(Calculator1);
            Calculator2Command = new ActionCommand(Calculator2);
            Calculator3Command = new ActionCommand(Calculator3);
            Calculator4Command = new ActionCommand(Calculator4);
            Calculator5Command = new ActionCommand(Calculator5);
            Calculator6Command = new ActionCommand(Calculator6);
            Calculator7Command = new ActionCommand(Calculator7);
            Calculator8Command = new ActionCommand(Calculator8);
            Calculator9Command = new ActionCommand(Calculator9);
            Calculator0Command = new ActionCommand(Calculator0);
            CalculatorClearCommand = new ActionCommand(CalculatorClear);
            CalculatorPointCommand = new ActionCommand(CalculatorPoint);

            ///  هااااااااااااااااااااااام
            _navigationStore.CurrentMenuButtonsChanged += _navigationStore_CurrentMenuButtonsChanged;
        }

        private void _navigationStore_CurrentMenuButtonsChanged()
        {
            OnPropertyChanged(nameof(CurrentMenuButtons));
        }

        private string GetCalculatorNumber(string number)
        {
            if (Qty == "0")
            {
                Qty = string.Empty;
                Qty = Qty + number;
            }
            else
            {
                Qty = Qty + number;
            }

            return Qty;
        }
        private void Calculator1()
        {
            GetCalculatorNumber("1");
        }
        private void Calculator2()
        {
            GetCalculatorNumber("2");
        }
        private void Calculator3()
        {
            GetCalculatorNumber("3");
        }
        private void Calculator4()
        {
            GetCalculatorNumber("4");
        }
        private void Calculator5()
        {
            GetCalculatorNumber("5");
        }
        private void Calculator6()
        {
            GetCalculatorNumber("6");
        }
        private void Calculator7()
        {
            GetCalculatorNumber("7");
        }
        private void Calculator8()
        {
            GetCalculatorNumber("8");
        }
        private void Calculator9()
        {
            GetCalculatorNumber("9");
        }
        private void Calculator0()
        {
            GetCalculatorNumber("0");
        }
        private void CalculatorClear()
        {
            Qty ="0";
        }
        private void CalculatorPoint()
        {
            if (!Qty.Contains("."))
            {
                GetCalculatorNumber(".");
            }
        }
    }
}
