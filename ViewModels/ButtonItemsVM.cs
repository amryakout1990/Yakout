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
        private readonly NavigationStore _navigationStore;

        private readonly SelectedStore _selectedStore;
        private ObservableCollection<Button> menuButtons;
        private CollectionViewSource MenuItemsCollection;
        public ICollectionView SourceCollection => MenuItemsCollection.View;

        private DataTable Table;

        //private ICommand AddItemsByCategoryIdCommand { get; }


        private SqlCommand _command;

        public SqlCommand Command
        {
            get { return _command; }
            set { _command = value; OnPropertyChanged(nameof(Command)); }
        }

        private DataRowView _rowView;

        public DataRowView RowView
        {
            get { return _rowView; }
            set { _rowView = value; OnPropertyChanged(nameof(RowView)); }
        }

        public ButtonItemsVM(NavigationStore navigationStore,SelectedStore selectedStore)
        {
            _navigationStore = navigationStore;

            _selectedStore = selectedStore;

            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (Command = new SqlCommand("", connection))
                {
                    Command.CommandText = "select * from Items where CategoryId='" + _selectedStore.SelectedPropertyStore.CategoryId + "'";
                    using (Table = new DataTable())
                    {
                        menuButtons = new ObservableCollection<Button>();
                        menuButtons.Clear();
                        Table.Load(Command.ExecuteReader());
                        for (int i = 0; i < Table.Rows.Count; i++)
                        {
                            Button menuButton = new Button();
                            menuButton.Content = Table.Rows[i][1].ToString();
                            menuButton.Tag = Table.Rows[i][0].ToString();
                            menuButton.Width = 100;
                            menuButton.Height = 100;
                            menuButton.Name = "btn" + Table.Rows[i][0].ToString();
                            menuButton.Click += MenuButton_Click;
                            menuButton.Style = (Style)menuButton.FindResource("PosButtonStyle");
                            menuButtons.Add(menuButton);
                        }
                        Button menuButtonBack = new Button();
                        menuButtonBack.Content = "Back";
                        menuButtonBack.Tag = "Back";
                        menuButtonBack.Width = 100;
                        menuButtonBack.Height = 100;
                        menuButtonBack.Name = "btnBack";
                        menuButtonBack.Style = (Style)menuButtonBack.FindResource("ButtonBackStyle");
                        menuButtonBack.Click += menuButtonBack_Click;
                        menuButtons.Add(menuButtonBack);
                        MenuItemsCollection = new CollectionViewSource { Source = menuButtons };
                    }
                }
            }

        }

        private void menuButtonBack_Click(object sender, RoutedEventArgs e)
        {  
            _navigationStore.CurrentMenuButtons = new ButtonCategoriesVM(_navigationStore);

        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Test");
            //Button btn = (Button)sender;
            //_selectedStore.SelectedItemStore.ItemId= btn.Tag.ToString()
            //_navigationStore.CurrentMenuButtons = new ButtonItemsVM(_navigationStore, _selectedStore);

            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (Command = new SqlCommand("", connection))
                {
                    Command.CommandText = "select * from Items";
                    using (Table = new DataTable())
                    {
                        Table.Load(Command.ExecuteReader());
                        RowView.BeginEdit();
                    }
                }
            }


        }
    }
}
