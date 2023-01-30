using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Yakout.ViewModels;
using Yakout.Stores;

namespace Yakout.Views
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        private DataTable table;
        //private DataAdapter adapter;
        private SqlCommand command;
        private int index;

         //private readonly UserStore _selectedUserStore;

        public Users()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tx1.Text.Trim() == "")
                {
                    MessageBox.Show("Enter All Fields");
                    tx1.Focus();
                }
                else if (tx2.Text.Trim() == "")
                {
                    MessageBox.Show("Enter All Fields");
                    tx2.Focus();
                }
                else if (tx3.Text.Trim() == "")
                {
                    MessageBox.Show("Enter All Fields");
                    tx3.Focus();
                }
                else if (tx4.Text.Trim() == "")
                {
                    MessageBox.Show("Enter All Fields");
                    tx4.Focus();
                }
                else if (tx5.Text.Trim() == "")
                {
                    MessageBox.Show("Enter All Fields");
                    tx5.Focus();
                }
                else if (tx6.Text.Trim() == "")
                {
                    MessageBox.Show("Enter All Fields");
                    tx6.Focus();
                }
                else
                {
                    if (index == 0)
                    {
                        using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter("select max(id)+1 from Users", connection))
                            {
                                table = new DataTable();
                                adapter.Fill(table);
                                int id;
                                string _id = table.Rows[0][0].ToString();
                                if (string.IsNullOrEmpty(_id))
                                {
                                    id = 1;
                                    inOrUpdate(connection, id);
                                }
                                else
                                {
                                    id = Convert.ToInt32(_id);
                                    inOrUpdate(connection, id);
                                }

                            }


                        }

                    }
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee + "");
            }

        }
        string New_id;

        private void inOrUpdate(SqlConnection connection, int id)
        {
            index = 0;
            using (command = new SqlCommand("", connection))
            {
                if (index == 0)
                {
                    command.CommandText = "insert into Users values(@1,@2,@3,@4,@5,@6,@7)";
                    command.Parameters.Clear();
                    //command.Parameters.Add("@1", SqlDbType.Int).Value = id;
                    //command.Parameters.Add("@2", SqlDbType.NVarChar).Value = UserName;
                    //command.Parameters.Add("@3", SqlDbType.NVarChar).Value = Password;
                    //command.Parameters.Add("@4", SqlDbType.NVarChar).Value = FullName;
                    //command.Parameters.Add("@5", SqlDbType.NVarChar).Value = JobDes;
                    //command.Parameters.Add("@6", SqlDbType.NVarChar).Value = Email;
                    //command.Parameters.Add("@7", SqlDbType.NVarChar).Value = Phone;
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Saved Successfuly");
                }
                else
                {
                    command.CommandText = "update Users set userName = @2 , password =@3 , fullName = @4 , jobDes = @5 , email = @6 , phone = @7 where id = " + Convert.ToInt32(New_id) + "";
                    command.Parameters.Clear();
                    command.Parameters.Add("@2", SqlDbType.NVarChar).Value = tx1.Text;
                    command.Parameters.Add("@3", SqlDbType.NVarChar).Value = tx2.Text;
                    command.Parameters.Add("@4", SqlDbType.NVarChar).Value = tx3.Text;
                    command.Parameters.Add("@5", SqlDbType.NVarChar).Value = tx4.Text;
                    command.Parameters.Add("@6", SqlDbType.NVarChar).Value = tx5.Text;
                    command.Parameters.Add("@7", SqlDbType.NVarChar).Value = tx6.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("updated Successfuly");
                }
            }

        }

        private void getData(int _index)
        {
            table = new DataTable();
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where id=" + _index + "", connection))
                {
                    table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        tx1.Text = table.Rows[0][1].ToString();
                        tx2.Text = table.Rows[0][2].ToString();
                        tx3.Text = table.Rows[0][3].ToString();
                        tx4.Text = table.Rows[0][4].ToString();
                        tx5.Text = table.Rows[0][5].ToString();
                        tx6.Text = table.Rows[0][6].ToString();
                    }

                }
            }
        }

    }
}
