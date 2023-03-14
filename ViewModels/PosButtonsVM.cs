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
    class PosButtonsVM : Utilities.ViewModelBase
    {
        public string ttt => _selectedPosStore?.SelectedPos.PosCategoryId ?? "h";
        private readonly SelectedPosStore _selectedPosStore;

        private readonly NavigationStore _navigationStore;

        private ObservableCollection<Button> menuButtons;
        private CollectionViewSource MenuItemsCollection;
        public ICollectionView SourceCollection => MenuItemsCollection.View;
        private Button menuButtonCategories;

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

        public PosButtonsVM(NavigationStore navigationStore, SelectedPosStore selectedPosStore)
        {
            Table2 = new DataTable();
            _navigationStore = navigationStore;
            _selectedPosStore = selectedPosStore;
            FillCategoriesIntoButtons();
            FillItemsIntoButtons();
            _selectedPosStore.SelectedPosStoreChanged += _selectedPosStore_SelectedPosStoreChanged;

        }

        private void _selectedPosStore_SelectedPosStoreChanged()
        {
            OnPropertyChanged(nameof(ttt));
            OnPropertyChanged(nameof(MenuItemsCollection));
            OnPropertyChanged(nameof(SourceCollection));
            OnPropertyChanged(nameof(menuButtonCategories));

            OnPropertyChanged(nameof(menuButtons2));
            OnPropertyChanged(nameof(MenuItemsCollection2));
            OnPropertyChanged(nameof(SourceCollection2));
            OnPropertyChanged(nameof(menuButtonItems));

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
                menuButtons= new ObservableCollection<Button>();
                menuButtons.Clear();
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    menuButtonCategories = new Button();
                    menuButtonCategories.Content = Table.Rows[i][1].ToString();
                    menuButtonCategories.Tag = Table.Rows[i][0].ToString();
                    menuButtonCategories.Name = "btn" + Table.Rows[i][1].ToString();
                    menuButtonCategories.Style = (Style)menuButtonCategories.FindResource("PosCategoriesButtonStyle");
                    menuButtonCategories.Click += menuButtonCategories_Click;
                    menuButtons.Add(menuButtonCategories);
                }
                MenuItemsCollection = new CollectionViewSource { Source = menuButtons };
            }

        }
        private void menuButtonCategories_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test2");
            //Button btn = (Button)sender;
            //_selectedPosStore.SelectedPos.PosCategoryId = btn.Tag.ToString();
            //FillItemsIntoButtons();
            //_selectedPosStore.SelectedPosStoreChanged += _selectedPosStore_SelectedPosStoreChanged;
            //MessageBox.Show(_selectedPosStore.SelectedPos.PosCategoryId + "f");
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
                            if (CodeBehindDataTable.Rows.Count>0)
                            {
                                using (Table = new DataTable())
                                {
                                    Table = CodeBehindDataTable;
                                    menuButtons2 = new ObservableCollection<Button>();
                                    menuButtons2.Clear();
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
                                        menuButtons2.Add(menuButtonItems);
                                    }

                                    MenuItemsCollection2 = new CollectionViewSource { Source = menuButtons2 };
                                }

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

    }
}
