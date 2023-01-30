using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yakout.Commands;
using Yakout.Models;
using Yakout.Stores;
using Yakout.Utilities;
using Yakout.ViewModels;

namespace Yakout.Views
{
    /// <summary>
    /// Interaction logic for UserSelect.xaml
    /// </summary>
    public partial class UserSelect : UserControl
    {
        private DataTable table;

        public UserSelect()
        {
            InitializeComponent();
            Loaded += UserSelect_Loaded;
            
        }

        private void UserSelect_Loaded(object sender, RoutedEventArgs e)
        {
            //using (SqlConnection connection=new SqlConnection(Models.connectionString.cs))
            //{
            //    using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users", connection))
            //    {
            //        table = new DataTable();
            //        adapter.Fill(table);
            //        gridUserSelect.ItemsSource= table.AsDataView();
            //    }
            //}
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where userName like '%"+tb.Text+"%'", connection))
                {
                    table = new DataTable();
                    adapter.Fill(table);
                    gridUserSelect.ItemsSource = table.AsDataView();
                }
            }

        }


        //private void gridUserSelect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (sender != null)
        //    {
        //        DataGrid grid = sender as DataGrid;
        //        if (grid != null && grid.SelectedItem != null && grid.SelectedItems.Count == 1)
        //        {
        //            DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
        //            DataRowView dataRow = (DataRowView)row.Item;
        //            UsersStore users = new UsersStore()
        //            {
        //                UserName = dataRow[1].ToString(),
        //                Password = dataRow[2].ToString(),
        //                FullName = dataRow[3].ToString(),
        //                JobDes = dataRow[4].ToString(),
        //                Email = dataRow[5].ToString(),
        //                Phone = dataRow[6].ToString()
        //            };

        //            SelectedUserStore selectedUserStore = new SelectedUserStore();
        //            selectedUserStore.SelectedUser = users;

        //            //var com = (NavigationVM)DataContext;
        //            //if (com.UsersCommand2.CanExecute(true))
        //            //{
        //            //    com.Execute(users2);
        //            //}
        //        }
        //    }
        //}

    }
}
