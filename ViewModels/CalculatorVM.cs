using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Yakout.Commands;
using Yakout.Models;
using Yakout.Stores;

namespace Yakout.ViewModels
{
    class CalculatorVM:Utilities.ViewModelBase
    {
        private ObservableCollection<Button> menuButtons2;
        private CollectionViewSource MenuItemsCollection2;
        public ICollectionView SourceCollection2 => MenuItemsCollection2?.View;
        private Button menuButtonItems;
        private DataTable _table;
        private ICommand ShowItemsByCategoryIdCommand { get; }

        private SqlCommand _command;

        public SqlCommand Command
        {
            get { return _command; }
            set { _command = value; OnPropertyChanged(nameof(Command)); }
        }

        public DataTable Table
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(nameof(Table)); }
        }
        private DataTable Table2;

        private DataTable _CodeBehindDataTable;
        public DataTable CodeBehindDataTable
        {
            get { return _CodeBehindDataTable; }
            set { _CodeBehindDataTable = value; OnPropertyChanged(); }
        }

        private readonly SelectedPosStore _selectedPosStore;

        private string _qty;
        public string Qty
        {
            get { return _qty; }
            set { _qty = value; OnPropertyChanged(nameof(Qty)); }
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

        public CalculatorVM(SelectedPosStore selectedPosStore)
        {
            _selectedPosStore = selectedPosStore;
            //_selectedItemStore.SelectedItemChanged += _selectedItemStore_SelectedItemChanged;
            Qty = "0";

            ActivateCommands();


        }
        private void _selectedItemStore_SelectedItemChanged()
        {
            /// حدث بينشط حدث بينشط خاصية
            //OnPropertyChanged(nameof(ItemName));
        }
        private void ActivateCommands()
        {
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
            Qty = "0";
        }
        private void CalculatorPoint()
        {
            //if (!Qty.Contains("."))
            //{
            //    GetCalculatorNumber(".");
            //}

            PosStore store = new PosStore();
            store.PosCategoryId = "3";
            _selectedPosStore.SelectedPos = store;
            FillItemsIntoButtons();
        }
        private void FillItemsIntoButtons()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (Command = new SqlCommand("", connection))
                {
                    if (!string.IsNullOrEmpty(_selectedPosStore.SelectedPos.PosCategoryId))
                    {
                        Command.CommandText = "select ItemId,ItemName,Price from Items where CategoryId='" + _selectedPosStore.SelectedPos.PosCategoryId + "' ";
                        using (CodeBehindDataTable = new DataTable())
                        {
                            CodeBehindDataTable.Load(Command.ExecuteReader());
                            if (CodeBehindDataTable.Rows.Count > 0)
                            {
                                PosStore store = new PosStore();
                                store.PosTable = CodeBehindDataTable;
                                _selectedPosStore.SelectedPos = store;
                            }
                        }

                    }
                }
            }


        }

    }
}
