using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yakout.Commands;
using Yakout.Stores;
using Yakout.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Windows;
using System.ComponentModel;
using System.Collections;

namespace Yakout.Views
{
    class ItemsSelectVM :Utilities.ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();


        public bool HasErrors => throw new NotImplementedException();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        EnumerableRowCollection<DataRow> rows;

        private DataView _dataViewGrid;

        public DataView DataViewGrid
        {
            get { return _dataViewGrid; }
            set { _dataViewGrid = value; OnPropertyChanged(); }
        }

        private string _filterTextBox;

        public string FilterTextBox
        {
            get { return _filterTextBox; }
            set { _filterTextBox = value; OnPropertyChanged(); }
        }

        private readonly NavigationStore _navigationStore;
        private readonly SelectedItemStore _selectedItemStore;
        private DataTable _table;

        public DataTable table
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(); }
        }

        private DataTable _dataTableForGrid;

        public DataTable DataTableForGrid
        {
            get { return table; }
            set { _dataTableForGrid = value;OnPropertyChanged(); }
        }

        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; OnPropertyChanged(); }
        }
        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; OnPropertyChanged(); }
        }
        private string _price;

        public string Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }
        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(); }
        }

        private DataRowView _row;

        public DataRowView row
        {
            get { return _row; }
            set { _row = value; OnPropertyChanged(); }
        }

        public ICommand NavigateItemsCommand { get; private set; }
        public ICommand NavigateItemsAfterSelectionCommand { get; private set; }


        public ItemsSelectVM(NavigationStore navigationStore, SelectedItemStore selectedItemStore)
        {
            LoadItems();
            _navigationStore = navigationStore;
            _selectedItemStore = selectedItemStore;
            NavigateItemsCommand = new ActionCommand(NavigateItems);
            NavigateItemsAfterSelectionCommand = new ActionCommand(NavigateItemsAfterSelection);

            DataViewGrid = new DataView(table);
            if (FilterTextBox != null)
            {
                //DataViewGrid.RowFilter = "ItemName='"+FilterTextBox+"'";

                rows = from tab in table.AsEnumerable()
                       where tab.Field<string>("ItemName").Contains(FilterTextBox)
                       select tab;

                DataViewGrid = rows.AsDataView();

            }
            else
            {
            }

        }
        
        private void NavigateItemsAfterSelection()
        {
            _selectedItemStore.SelectedItem.ItemId = Convert.ToInt32(row[0].ToString());
            _selectedItemStore.SelectedItem.ItemName = row[1].ToString();
            _selectedItemStore.SelectedItem.CategoryId = Convert.ToInt32(row[2].ToString());
            _selectedItemStore.SelectedItem.CategoryName = row[3].ToString();
            _selectedItemStore.SelectedItem.Price = row[4].ToString();
            _selectedItemStore.SelectedItem.Notes = row[5].ToString();
            _selectedItemStore.SelectedItem.Image = (byte[])row[6];
            _navigationStore.CurrentViewModel = new ItemsVM(_navigationStore, _selectedItemStore);
        }

        private void NavigateItems()
        {
            _navigationStore.CurrentViewModel = new ItemsVM(_navigationStore);
        }

        private void LoadItems()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "QueryItemsSelect";
                    command.CommandType = CommandType.StoredProcedure;
                    table = new DataTable();
                    table.Load(command.ExecuteReader());
                }
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
