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
        private readonly SelectedItemStore _selectedItemStore;

        //public string ItemId=> _selectedPosStore?.PosStore.PosCategoryId ?? "emp";

        private ObservableCollection<Button> menuButtons2;
        private CollectionViewSource MenuItemsCollection2;
        public ICollectionView SourceCollection2 => MenuItemsCollection2?.View;
        private Button menuButtonCategories;

        private SelectedPosStore _selectedPosStore;
        public CalculatorVM CalculatorVM { get; }
        public PosButtonsVM PosButtonsVM { get; }

        private ObservableCollection<Button> menuButtons;
        private CollectionViewSource MenuItemsCollection;
        private ICollectionView _sourceCollection;

        public ICollectionView SourceCollection
        {
            get { return _sourceCollection; }
            set { _sourceCollection = value;OnPropertyChanged(); }
        }

        private Button menuButtonItems;

        private readonly NavigationStore _navigationStore;


        /// <summary>
        /// دا اللي مربوط في الفيوووو الرئيسي
        /// </summary>

        public ViewModelBase CurrentMenuButtons => _navigationStore.CurrentMenuButtons;

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

        public ICommand test { get; private set; }

        private string _categoryId;
        public string CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; OnPropertyChanged(); }
        }

        private DataRow _row;

        public DataRow Row
        {
            get { return _row; }
            set { _row = value; OnPropertyChanged(); }
        }

        private DataTable _gridBoundDataTable;
        public DataTable GridBoundDataTable
        {
            get
            {
                if (_gridBoundDataTable != null)
                {
                    return _gridBoundDataTable;
                }
                else return new DataTable();
            }
            set { _gridBoundDataTable = value; OnPropertyChanged(); }
        }

        private DataTable _CodeBehindDataTable;
        public DataTable CodeBehindDataTable
        {
            get { return _CodeBehindDataTable; }
            set { _CodeBehindDataTable = value; OnPropertyChanged(); }
        }



        public PosVM(NavigationStore navigationStore, SelectedItemStore selectedItemStore)
        {
            _selectedItemStore = selectedItemStore;
            _navigationStore = navigationStore;
            test = new ActionCommand(tt);
            _selectedItemStore.SelectedItemChanged += _selectedItemStore_SelectedItemChanged;
            _selectedPosStore = new SelectedPosStore();
            //PosButtonsVM = new PosButtonsVM(_navigationStore, _selectedPosStore);
            CalculatorVM = new CalculatorVM(_selectedPosStore);
            FillCategoriesIntoButtons();
            AddColumns();
            //FillItemsIntoButtons("1");
        }

        private void _selectedItemStore_SelectedItemChanged()
        {
            //OnPropertyChanged(nameof(ItemId));

        }

        private void tt()
        {
            //MessageBox.Show(ItemId);
        }

        private void AddColumns()
        {
            GridBoundDataTable.Columns.Add("ItemId");
            GridBoundDataTable.Columns.Add("ItemName");
            GridBoundDataTable.Columns.Add("Price");

        }
        private DataTable GetCategoriesFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (Command = new SqlCommand("", connection))
                {
                    Command.CommandText = "select * from Categories";
                    using (Table = new DataTable())
                    {
                        Table.Load(Command.ExecuteReader());
                        return Table;
                    }
                }
            }
        }
        private void FillCategoriesIntoButtons()
        {
            using (Table = new DataTable())
            {
                Table = GetCategoriesFromDatabase();
                menuButtons2 = new ObservableCollection<Button>();
                menuButtons2.Clear();
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    menuButtonCategories = new Button();
                    menuButtonCategories.Content = Table.Rows[i][1].ToString();
                    menuButtonCategories.Tag = Table.Rows[i][0].ToString();
                    menuButtonCategories.Name = "btn" + Table.Rows[i][1].ToString();
                    menuButtonCategories.Style = (Style)menuButtonCategories.FindResource("PosCategoriesButtonStyle");
                    menuButtonCategories.Click += menuButtonCategories_Click;
                    menuButtons2.Add(menuButtonCategories);
                }
                MenuItemsCollection2 = new CollectionViewSource { Source = menuButtons2 };
            }

        }
        private void menuButtonCategories_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            FillItemsIntoButtons(btn.Tag.ToString());

            //_selectedPosStore.SelectedPos.PosCategoryId = btn.Tag.ToString();
            //FillItemsIntoButtons();
            //_selectedPosStore.SelectedPosStoreChanged += _selectedPosStore_SelectedPosStoreChanged;
            //MessageBox.Show(_selectedPosStore.SelectedPos.PosCategoryId + "f");
        }

        private void FillItemsIntoButtons(string id)
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (Command = new SqlCommand("", connection))
                {
                    Command.CommandText = "select ItemId,ItemName,Price from Items where CategoryId='" + id + "' ";
                    using (CodeBehindDataTable = new DataTable())
                    {
                        CodeBehindDataTable.Load(Command.ExecuteReader());
                        if (CodeBehindDataTable.Rows.Count > 0)
                        {
                            using (Table = new DataTable())
                            {
                                Table = CodeBehindDataTable;
                                menuButtons = new ObservableCollection<Button>();
                                menuButtons.Clear();
                                for (int i = 0; i < Table.Rows.Count; i++)
                                {
                                    menuButtonItems = new Button();
                                    menuButtonItems.Content = Table.Rows[i][1].ToString();
                                    menuButtonItems.Width = 100;
                                    menuButtonItems.Height = 100;
                                    menuButtonItems.Tag = Table.Rows[i][0].ToString();
                                    menuButtonItems.Name = "btn" + Table.Rows[i][0].ToString();
                                    menuButtonItems.Style = (Style)menuButtonItems.FindResource("PosButtonStyle");
                                    menuButtonItems.Click += menuButtonItems_Click;
                                    menuButtons.Add(menuButtonItems);
                                }

                                MenuItemsCollection = new CollectionViewSource { Source = menuButtons };
                                SourceCollection = MenuItemsCollection.View;
                            }

                        }
                    }
                }
            }


        }
        private void menuButtonItems_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test");
        }

        //private void GetItem()
        //{
        //    using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
        //    {
        //        connection.Open();
        //        using (Command = new SqlCommand("", connection))
        //        {
        //            Command.CommandText = "select * from Items where ItemId=" + Convert.ToInt32(_selectedItemStore.SelectedItem.ItemId) + " ";
        //            using (Table = new DataTable())
        //            {
        //                Table.Load(Command.ExecuteReader());
        //                if (Table.Rows.Count>0)
        //                {
        //                    ItemId = Table.Rows[0][0].ToString();
        //                    //Row = GridBoundDataTable.NewRow();
        //                    //Row[0] = Convert.ToInt32(Table.Rows[0][0].ToString());
        //                    //Row[1] = Table.Rows[0][1].ToString();
        //                    //Row[2] = Table.Rows[0][3].ToString();
        //                    //GridBoundDataTable.Rows.Add(Row);

        //                }
        //            }
        //        }
        //    }

        //}
        //CalculatorVM = new CalculatorVM(_selectedItemStore);

        //private void _navigationStore_CurrentMenuButtonsChanged()
        //{
        //    OnPropertyChanged(nameof(CurrentMenuButtons));
        //}
        //_navigationStore.CurrentMenuButtonsChanged += _navigationStore_CurrentMenuButtonsChanged;
        //_navigationStore.CurrentMenuButtons = new ButtonCategoriesVM(_navigationStore, _selectedItemStore);


    }
}
