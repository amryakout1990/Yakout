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
using Yakout.Stores;

namespace Yakout.ViewModels
{
    class ButtonItemsVM:Utilities.ViewModelBase
    {
        public string ttt => _selectedItemStore?.SelectedItem.CategoryId.ToString() ?? "k";

        private readonly NavigationStore _navigationStore;
        private readonly SelectedItemStore _selectedItemStore;

        private ObservableCollection<Button> menuButtons;
        private CollectionViewSource MenuItemsCollection;
        public ICollectionView SourceCollection => MenuItemsCollection.View;
        private Button menuButton;
        private DataTable Table;

        private SqlCommand _command;
        public SqlCommand Command
        {
            get { return _command; }
            set { _command = value; OnPropertyChanged(nameof(Command)); }
        }

        private DataTable _CodeBehindDataTable;
        public DataTable CodeBehindDataTable
        {
            get { return _CodeBehindDataTable; }
            set { _CodeBehindDataTable = value; OnPropertyChanged(); }
        }
        //private DataTable _transferDataTable;
        //public DataTable TransferDataTable
        //{
        //    get { return _transferDataTable; }
        //    set { _transferDataTable = value; OnPropertyChanged(); }
        //}


        public ButtonItemsVM(NavigationStore navigationStore, SelectedItemStore selectedItemStore)
        {
            _navigationStore = navigationStore;
            _selectedItemStore = selectedItemStore;
            FillItemsIntoButtons();
            _selectedItemStore.SelectedItemChanged += _selectedItemStore_SelectedItemChanged;

            //TransferDataTable = new DataTable();
            //TransferDataTable.Columns.Add("ItemId");
            //TransferDataTable.Columns.Add("ItemName");
            //TransferDataTable.Columns.Add("Price");

        }

        private void _selectedItemStore_SelectedItemChanged()
        {
            OnPropertyChanged(nameof(SourceCollection));
        }

        private DataTable GetItemsFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (Command = new SqlCommand("", connection))
                {
                    Command.CommandText = "select ItemId,ItemName,Price from Items where CategoryId='" + _selectedItemStore.SelectedItem.CategoryId.ToString()+ "' ";
                    using (CodeBehindDataTable = new DataTable())
                    {
                        CodeBehindDataTable.Load(Command.ExecuteReader());

                        return CodeBehindDataTable;
                    }
                }
            }
        }

        private void FillItemsIntoButtons()
        {
            using (Table = new DataTable())
            {
                Table = GetItemsFromDatabase();
                menuButtons = new ObservableCollection<Button>();
                menuButtons.Clear();
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    menuButton = new Button();
                    menuButton.Content = Table.Rows[i][1].ToString();
                    menuButton.Width = 100;
                    menuButton.Height = 100;
                    menuButton.Tag = Table.Rows[i][0].ToString();
                    menuButton.Name = "btn" + Table.Rows[i][0].ToString();
                    menuButton.Style = (Style)menuButton.FindResource("PosButtonStyle");
                    menuButton.Click += MenuButton_Click;
                    menuButtons.Add(menuButton);
                }
                menuButton = new Button();
                menuButton.Content = "Back";
                menuButton.Width = 100;
                menuButton.Height = 100;
                menuButton.Name = "menuButtonBack";
                menuButton.Style = (Style)menuButton.FindResource("PosButtonStyle");
                menuButton.Click += menuButtonBack_Click;
                menuButtons.Add(menuButton);

                MenuItemsCollection = new CollectionViewSource { Source = menuButtons };
            }

        }

        private void menuButtonBack_Click(object sender, RoutedEventArgs e)
        {
            _navigationStore.CurrentMenuButtons = new ButtonCategoriesVM(_navigationStore, _selectedItemStore);

        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;

            _selectedItemStore.SelectedItem.ItemId =Convert.ToInt32(btn.Tag);


        }
    }
}
