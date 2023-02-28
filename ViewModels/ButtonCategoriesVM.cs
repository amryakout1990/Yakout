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
using Yakout.Utilities;

namespace Yakout.ViewModels
{
    class ButtonCategoriesVM:Utilities.ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private ObservableCollection<Button> menuButtons;
        private CollectionViewSource MenuItemsCollection;
        public ICollectionView SourceCollection => MenuItemsCollection.View;
        private Button menuButton;
        private DataTable _table;
        private ICommand ShowItemsByCategoryIdCommand { get; }
        private SelectedStore _selectedStore;

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

        public ButtonCategoriesVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _selectedStore = new SelectedStore();
            //ShowItemsByCategoryIdCommand = new ActionCommand(ShowItemsByCategoryId);

            FillCategoriesIntoButtons();

        }

        //private void ShowItemsByCategoryId()
        //{


        //    _navigationStore.CurrentMenuButtons = new ButtonItemsVM(_navigationStore,_selectedStore);
        //}

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
                menuButtons = new ObservableCollection<Button>();
                menuButtons.Clear();
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    menuButton = new Button();
                    menuButton.Content = Table.Rows[i][1].ToString();
                    menuButton.Width = 100;
                    menuButton.Height = 100;
                    menuButton.Tag = Table.Rows[i][0].ToString();
                    menuButton.Name = "btn" + Table.Rows[i][1].ToString();
                    menuButton.Style = (Style)menuButton.FindResource("PosButtonStyle");
                    menuButton.Click += MenuButton_Click;
                    menuButton.Command =ShowItemsByCategoryIdCommand;
                    menuButtons.Add(menuButton);
                }

                MenuItemsCollection = new CollectionViewSource { Source = menuButtons };
            }

        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            _selectedStore.SelectedPropertyStore.CategoryId = btn.Tag.ToString();
            _navigationStore.CurrentMenuButtons = new ButtonItemsVM(_navigationStore, _selectedStore);


        }


    }
}
