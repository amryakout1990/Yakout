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

    }
}
